using MediatR;
using Study402Online.Common.Model;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 订单创建命令
/// </summary>
public record OrderCreateCommand : IRequest<Result<int>>
{
    /// <summary>
    /// 下单客户 IP
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// 总价格
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// 订单类型
    /// </summary>
    public OrderType OrderType { get; set; }

    /// <summary>
    /// 订单名称
    /// </summary>
    public string OrderName { get; set; }

    /// <summary>
    /// 订单描述
    /// </summary>
    public string OrderDescription { get; set; }

    /// <summary>
    /// 订单项
    /// </summary>
    public IEnumerable<OrderItemModel> OrderItems { get; set; }

    /// <summary>
    /// 外部业务系统 ID
    /// </summary>
    public string ExternalBusinessId { get; set; }
}