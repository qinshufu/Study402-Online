using System.Collections.Specialized;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Application.Commands;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 创建订单
    /// </summary>
    /// <returns>订单 ID</returns>
    [HttpPost("create")]
    public Task<Result<int>> CreateOrder([FromBody] OrderCreateCommand comand) => _mediator.Send(comand);

    /// <summary>
    /// 支付订单
    /// </summary>
    /// <returns></returns>
    [HttpPost("pay")]
    public Task<Result<PaymentModel>> PayOrder([FromBody] OrderPayCommand command) => _mediator.Send(command);

    /// <summary>
    /// 接受微信支付完成的通知
    /// </summary>
    [HttpPost("wechat_pay_callback")]
    public Task<PaymentNotificationResponse> ReceiveWechatPaymentResult(
        [FromBody] NameValueCollection nameValueCollection) =>
        _mediator.Send(new PaymentResultReceiveCommand(nameValueCollection, PayVendor.WeChatPay));

    /// <summary>
    /// 获取支付结果
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpGet("payment_result")]
    public Task<Result<OrderPayRecord>> QueryPaymentResult([FromQuery] PaymentResultQueryCommand command) =>
        _mediator.Send(command);
}