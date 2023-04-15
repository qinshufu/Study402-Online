using System.Collections.Specialized;
using MediatR;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 支付结果回调命令处理
/// </summary>
/// <param name="value"></param>
public record PaymentResultReceiveCommand
    (NameValueCollection PaymentResult, PayVendor PayVendor) : IRequest<PaymentNotificationResponse>;