using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Study402Online.OrderService.Api.Application.Services;
using Study402Online.OrderService.Api.Instructure;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 处理支付结果通知
/// </summary>
public class PaymentResultHandler : IRequestHandler<PaymentResultReceiveCommand, PaymentNotificationResponse>
{
    private readonly OrderServiceDbContext _dbContext;

    private readonly HttpContextAccessor _httpContextAccessor;

    private readonly IOptions<WechatPayOptions> _wechatOptions;

    private readonly ISignatureService _signatureService;

    public PaymentResultHandler(
        OrderServiceDbContext dbContext, HttpContextAccessor httpContextAccessor,
        IOptions<WechatPayOptions> wechatOptions, ISignatureService signatureService)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _wechatOptions = wechatOptions;
        _signatureService = signatureService;
    }

    public async Task<PaymentNotificationResponse> Handle(
        PaymentResultReceiveCommand request,
        CancellationToken cancellationToken) => request.PayVendor switch
    {
        PayVendor.AliPay => await HanleAliPaymentResultAsync(request, cancellationToken),
        PayVendor.WeChatPay => await HandleWechatPaymentResultAsync(request, cancellationToken),
        _ => new PaymentNotificationResponse("FAILED", "失败")
    };

    private async Task<PaymentNotificationResponse> HandleWechatPaymentResultAsync(PaymentResultReceiveCommand request,
        CancellationToken cancellationToken)
    {
        if (await VerifyWechatSignatureAsync() is false)
            return new PaymentNotificationResponse("FAILURE", "校验微信签名失败");

        var textData = await DecryptionWechatNotificationAsync();

        if (textData is null)
            return new PaymentNotificationResponse("FAILURE", "无法正确解码微信支付通知");

        var jsonData = JsonDocument.Parse(textData).RootElement;

        var payNo = jsonData.GetProperty("out_trade_no").GetString();
        var payStatus = jsonData.GetProperty("trade_state	").GetString();

        // 忽略中间状态，当支付到达完成状态以后删掉支付记录，也可以达到幂等性
        return payStatus switch
        {
            "SUCCESS" or "PAYERROR" or "CLOSED" or "REVOKED" => await HandlePaymentComplectionAsync(payNo!, payStatus),
            "USERPAYING" => await HandlePaymentStartAsync(payNo!, payStatus) //忽略
        };
    }

    /// <summary>
    /// 处理支付开始事件
    /// </summary>
    /// <param name="payNo"></param>
    /// <param name="payStatus"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task<PaymentNotificationResponse> HandlePaymentStartAsync(string payNo, string payStatus)
    {
        var payRecord = await GetPayRecordByPayNoAsync(payNo);
        var order = await GetOrderByPayNoAsync(payNo);

        if (payRecord is null || order is null)
            return new PaymentNotificationResponse("FAILURE", "没有找到对应的支付记录");

        payRecord.Status = PayStatus.DuringPayment;
        order.Status = OrderStatus.DuringPayment;

        _dbContext.Update(payRecord);
        _dbContext.Update(order);

        await _dbContext.SaveChangesAsync();

        return new PaymentNotificationResponse(PaymentNotificationResponse.SuccessCode, "succ");
    }

    private Task<OrderPayRecord?> GetPayRecordByPayNoAsync(string payNo) =>
        _dbContext.OrderPayRecords.SingleOrDefaultAsync(p => p.PayNo == long.Parse(payNo));

    /// <summary>
    /// 处理支付结束事件
    /// </summary>
    /// <param name="payNo"></param>
    /// <param name="payStatus"></param>
    /// <returns></returns>
    private async Task<PaymentNotificationResponse> HandlePaymentComplectionAsync(string payNo, string payStatus)
    {
        var order = await GetOrderByPayNoAsync(payNo);
        var payRecord = await GetPayRecordByPayNoAsync(payNo);

        if (order is null || payRecord is null)
            return new PaymentNotificationResponse("FAILURE", "制定支付记录不存在");

        // 是其他任何状态都报错，因为不是完成状态
        if (payStatus is not ("PAYERROR" or "CLOSED" or "REVOKED" or "SUCCESS"))
            return new PaymentNotificationResponse("FAILURE", "无效的支付状态");

        (order.Status, payRecord.Status) = payStatus switch
        {
            "PAYERROR" or "CLOSED" or "REVOKED" => (OrderStatus.PaymentFailed, PayStatus.PaymentFailure),
            "SUCCESS" => (OrderStatus.PaymentSuccessful, PayStatus.PaymentSuccessful),
            _ => throw new InvalidOperationException("不应该存在其他情况")
        };

        // 本来支付完成时间应该从支付通知数据中取，如果通知没有正常收到或者处理的话，那么这里的订单完成时间将和实际的订单完成时间有很大的距离，不过也不是啥大问题
        order.FinishTime = DateTime.Now;

        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();

        return new PaymentNotificationResponse(PaymentNotificationResponse.SuccessCode, "succ");
    }

    private Task<Order?> GetOrderByPayNoAsync(string payNo) =>
        _dbContext.Orders.Join(_dbContext.OrderPayRecords.Where(p => p.PayNo == long.Parse(payNo)), o => o.Id,
                p => p.OrderId,
                (o, _) => o)
            .SingleOrDefaultAsync();

    /// <summary>
    /// 解析 resource 字段下的加密数据，详情见 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
    /// </summary>
    /// <returns></returns>
    private async Task<string> DecryptionWechatNotificationAsync()
    {
        using var bodyStream = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
        var notificaitonJsonDocument =
            JsonDocument.Parse(await bodyStream.ReadToEndAsync()).RootElement.GetProperty("resource");

        var ciphertext = notificaitonJsonDocument.GetProperty("ciphertext").GetString();
        var associate = notificaitonJsonDocument.GetProperty("associated_data").GetString();
        var nonce = notificaitonJsonDocument.GetProperty("nonce").GetString();

        return await _signatureService.DecryptWechatDataAsync(ciphertext, associate, nonce);
    }

    /// <summary>
    /// 校验微信签名
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task<bool> VerifyWechatSignatureAsync()
    {
        using var bodyReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
        var body = bodyReader.ReadToEnd();

        if (body.EndsWith("\n") is false)
            body += "\n";

        var headers = _httpContextAccessor.HttpContext.Request.Headers;

        var serialCodeExists =
            headers.TryGetValue("Wechatpay-Serial", out var serialNumber);

        if (serialCodeExists is false || serialNumber.ToString() != _wechatOptions.Value.CertificateSerialNumber)
            return false;

        var time = headers["Wechatpay-Timestamp"].ToString();
        var code = headers["Wechatpay-Nonce"].ToString();

        var signatureBuilder = new StringBuilder();

        signatureBuilder.Append(time + "\n");
        signatureBuilder.Append(code + "\n");
        signatureBuilder.Append(body);

        var signature = Encoding.UTF8.GetBytes(signatureBuilder.ToString());

        return await _signatureService.VerifyWechatSignatureAsync(signature);
    }

    private async Task<PaymentNotificationResponse> HanleAliPaymentResultAsync(PaymentResultReceiveCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}