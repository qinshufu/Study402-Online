namespace Study402Online.OrderService.Model.ViewModels;

/// <summary>
/// 用户根据此模型进行支付
/// </summary>
public class PaymentModel
{
    /// <summary>
    /// 阿里支付链接
    /// </summary>
    public Uri AliPayUri { get; set; }

    /// <summary>
    /// 阿里支付二维码
    /// </summary>
    public string AliPayQrCode { get; set; }
}
