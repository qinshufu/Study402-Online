namespace Study402Online.OrderService.Model.ViewModels;

/// <summary>
/// 用户根据此模型进行支付
/// </summary>
public class PaymentModel
{
    /// <summary>
    /// 支付链接
    /// </summary>
    public Uri PayUri { get; set; }

    /// <summary>
    /// 支付二维码
    /// </summary>
    public string PayQrCode { get; set; }

    /// <summary>
    /// 支付流水号
    /// </summary>
    public long PayNo { get; set; }
}