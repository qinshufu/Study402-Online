using MediatR;
using Study402Online.Common.Model;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 支付结果查询命令
/// </summary>
public class PaymentResultQueryCommand : IRequest<Result<OrderPayRecord>>
{
    /// <summary>
    /// 支付流水号
    /// </summary>
    public long PayNo { get; set; }
}