namespace Study402Online.OrderService.Model.DateModels;

/// <summary>
/// 订单支付记录表
/// </summary>
public class OrderPayRecord
{
    public int Id { get; set; }

    /// <summary>
    /// 交易单号
    /// </summary>
    public int PayNo { get; set; }

    /// <summary>
    /// 第三方交易号
    /// </summary>
    public string ExternalPayNo { get; set; }

    /// <summary>
    /// 第三方支付渠道
    /// </summary>
    public string ExternalPayChannel { get; set; }

    /// <summary>
    /// 支付订单 ID
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// 订单名称
    /// </summary>
    public string OrderName { get; set; }

    /// <summary>
    /// 订单总价值
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// 币种
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
