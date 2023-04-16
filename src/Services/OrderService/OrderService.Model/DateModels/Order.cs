using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study402Online.OrderService.Model.DateModels;

/// <summary>
/// 订单
/// </summary>
public class Order
{
    public int Id { get; set; }


    /// <summary>
    /// 总价格
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 订单状态
    /// </summary>
    public OrderStatus Status { get; set; }

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
    /// 订单详情
    /// </summary>
    public string OrderDetail { get; set; }

    /// <summary>
    /// 外部业务 ID
    /// </summary>
    public string ExternalBusinessId { get; set; }

    /// <summary>
    /// 订单结束时间（订单支付完成）
    /// </summary>
    public DateTime FinishTime { get; set; }
}

/// <summary>
/// 订单类型
/// </summary>
public enum OrderType
{
    None,

    /// <summary>
    /// 课程
    /// </summary>
    Course,

    /// <summary>
    /// 学习资料
    /// </summary>
    Materials
}

/// <summary>
/// 订单状态
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// 等待支付
    /// </summary>
    WaitingForPayment,

    /// <summary>
    /// 支付中
    /// </summary>
    DuringPayment,

    /// <summary>
    /// 支付完成
    /// </summary>
    PaymentSuccessful,

    /// <summary>
    /// 支付失败
    /// </summary>
    PaymentFailed
}