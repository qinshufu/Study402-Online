using System.Web;
using IdGen;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Application.Clients;
using Study402Online.OrderService.Api.Instructure;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 订单支付处理器
/// </summary>
public class OrderPayHandler : IRequestHandler<OrderPayCommand, Result<PaymentModel>>
{
    private readonly OrderServiceDbContext _dbContext;

    private readonly IOptions<WechatPayOptions> _options;

    private readonly IWechatPayApiClient _payApiClient;

    private readonly QRCodeGenerator _qrCodeGenerator;

    private readonly IdGenerator _idGenerator;


    public OrderPayHandler(
        OrderServiceDbContext dbContext, IOptions<WechatPayOptions> options,
        IWechatPayApiClient payApiClient, QRCodeGenerator qrCodeGenerator, IdGenerator idGenerator)
    {
        _dbContext = dbContext;
        _options = options;
        _payApiClient = payApiClient;
        _qrCodeGenerator = qrCodeGenerator;
        _idGenerator = idGenerator;
    }

    public async Task<Result<PaymentModel>>
        Handle(OrderPayCommand request, CancellationToken cancellationToken) => request.PayVendor switch

    {
        PayVendor.AliPay => await PayOrderByAliPayAsync(request, cancellationToken),
        PayVendor.WeChatPay => await PayOrderByWechatPay(request, cancellationToken),
        _ => ResultFactory.Fail<PaymentModel>("无效的第三方支付方式")
    };

    private async Task<Result<PaymentModel>> PayOrderByWechatPay(
        OrderPayCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.SingleOrDefaultAsync(r => r.Id == request.OrderId);

        if (order is null)
            return ResultFactory.Fail<PaymentModel>("订单不存在");

        var payRecordExists = await _dbContext.OrderPayRecords.AnyAsync(r => r.OrderId == request.OrderId);

        if (payRecordExists)
            return ResultFactory.Fail<PaymentModel>("指定订单支付记录已存在");

        var payRecord = new OrderPayRecord()
        {
            OrderId = order.Id,
            CreateTime = DateTime.Now,
            TotalPrice = order.TotalPrice,
            Currency = "CNY",
            ExternalPayChannel = "WechatPay",
            OrderName = order.OrderName,
            PayNo = _idGenerator.CreateId()
        };

        await _dbContext.AddAsync(payRecord);
        await _dbContext.SaveChangesAsync();

        // 其实为订单声明一个类型会更好，但是这是一个无聊的打字工作，所以直接匿名类型
        var createWechatOrderParams = new
        {
            mchid = _options.Value.MerchantId,
            out_trade_no = payRecord.PayNo,
            appid = _options.Value.AppId,
            description = order.OrderName,
            notify_url = _options.Value.NotifyUri,
            amount = new
            {
                total = order.TotalPrice,
                currency = "CNY"
            },
            payer = new
            {
                payer_client_ip = request.ClientId,
                h5_info = new
                {
                    type = "Wap"
                }
            }
        };

        var payUri = await _payApiClient.CreateOrderAsync(createWechatOrderParams);
        var prePayId = HttpUtility.ParseQueryString(payUri).Get("prepay_id");

        if (prePayId is null)
            return ResultFactory.Fail<PaymentModel>("获取订单支付链接失败");

        payRecord.ExternalPayNo = prePayId;
        await _dbContext.SaveChangesAsync();

        using var qrcodeData = _qrCodeGenerator.CreateQrCode(payUri, QRCodeGenerator.ECCLevel.Q);
        using var qrcode = new PngByteQRCode(qrcodeData);
        var qrcodePicture = qrcode.GetGraphic(20);
        var qrcodeBase64 = Base64UrlEncoder.Encode(qrcodePicture);

        return ResultFactory.Success(new PaymentModel()
        {
            PayNo = payRecord.PayNo,
            PayUri = new Uri(payUri),
            PayQrCode = qrcodeBase64
        });
    }

    private Task<Result<PaymentModel>> PayOrderByAliPayAsync(
        OrderPayCommand request,
        CancellationToken cancellationToken)
    {
        // TODO 实现 支付宝 支付
        throw new NotImplementedException();
    }
}