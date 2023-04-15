using MediatR;
using Study402Online.Common.Model;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 订单支付命令
/// </summary>
public class OrderPayCommand : IRequest<Result<PaymentModel>>
{
    /// <summary>
    /// 支付厂商
    /// </summary>
    public PayVendor PayVendor { get; set; }

    /// <summary>
    /// 订单 ID
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// 客户端 IP
    /// </summary>
    public string ClientId { get; set; }
}

/// <summary>
/// 支付厂商
/// </summary>
public enum PayVendor
{
    None,

    /// <summary>
    /// 微信支付
    /// </summary>
    WeChatPay,

    /// <summary>
    /// 阿里支付
    /// </summary>
    AliPay
}