using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Domain.Events;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Application.Clients;
using Study402Online.OrderService.Api.Instructure;
using Study402Online.OrderService.Model.DateModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 支付结果查询处理器
/// </summary>
public class PaymentResultQueryHandler : IRequestHandler<PaymentResultQueryCommand, Result<OrderPayRecord>>
{
    private readonly OrderServiceDbContext _dbContext;

    private readonly IOptions<WechatPayOptions> _options;

    private readonly IWechatPayApiClient _payApiClient;
    private readonly IBus _bus;

    public PaymentResultQueryHandler(
        OrderServiceDbContext dbContext, IOptions<WechatPayOptions> options,
        IWechatPayApiClient payApiClient, IBus bus)
    {
        _dbContext = dbContext;
        _options = options;
        _payApiClient = payApiClient;
        _bus = bus;
    }

    public async Task<Result<OrderPayRecord>> Handle(
        PaymentResultQueryCommand request,
        CancellationToken cancellationToken)
    {
        var record = await _dbContext.OrderPayRecords.SingleOrDefaultAsync(r => r.PayNo == request.PayNo);

        if (record is null)
            return ResultFactory.Fail<OrderPayRecord>("支付记录不存在");

        // 如果支付操作没有结束，则向支付商查询订单支付状态
        // 该操作是因为，支付商的支付通知可能没有正常接受到或成功处理，所以支付才没有结束
        if (record.Status is PayStatus.DuringPayment or PayStatus.WaitingForPayment)
            await UpdateOrderStatusAsync(record);

        return ResultFactory.Success(record);
    }

    private Task UpdateOrderStatusAsync(OrderPayRecord payRecord)
        => payRecord.ExternalPayChannel switch
        {
            "WechatPay" => UpdateOrderStatusFromWechatPayAsync(payRecord),
            "AliPay" => UpdateOrderStatusFromAliPayAsync(payRecord),
            _ => throw new InvalidOperationException("不支持的支付商")
        };

    private Task UpdateOrderStatusFromAliPayAsync(OrderPayRecord payRecord)
    {
        throw new NotImplementedException();
    }

    private async Task UpdateOrderStatusFromWechatPayAsync(OrderPayRecord payRecord)
    {
        var result = await _payApiClient.QueryOrderStatusAsync(payRecord.ExternalPayNo, _options.Value.MerchantId);
        var payStatus = result.GetProperty("out_trade_no").GetString();

        var order = await _dbContext.Orders.SingleAsync(o => o.Id == payRecord.OrderId);

        // 如果本地记录这个支付还没有开始，而支付商说他已经开始了，那么这里说明通知没有收到
        // 此时更新状态为支付中
        if (payStatus is "USERPAYING" && payRecord.Status is PayStatus.WaitingForPayment)
        {
            payRecord.Status = PayStatus.DuringPayment;
            order.Status = OrderStatus.DuringPayment;
        }

        // 当支付已经结束以后，本地的支付状态还没有改变
        if (payStatus is "SUCCESS" or "REFUND" or "NOTPAY" or "CLOSED" or "REVOKED" or "PAYERROR"
            && payRecord.Status is PayStatus.WaitingForPayment or PayStatus.WaitingForPayment)
        {
            var paySuccess = payStatus is "SUCCESS";

            payRecord.Status = paySuccess ? PayStatus.PaymentSuccessful : PayStatus.PaymentFailure;
            order.Status = paySuccess ? OrderStatus.PaymentSuccessful : OrderStatus.PaymentFailed;
            order.FinishTime = DateTime.Now;

            // FIXME: 这里可能会存在的问题，当事务提交失败，然而这里的消息已经发送了，状态就不一致了
            await _bus.Publish(new CoursePaymentCompletedEvent { IsPaymentSuccessful = paySuccess, CourseSelectionId = order.ExternalBusinessId });
        }

        _dbContext.Add(payRecord);
        _dbContext.Add(order);
        await _dbContext.SaveChangesAsync();
    }
}