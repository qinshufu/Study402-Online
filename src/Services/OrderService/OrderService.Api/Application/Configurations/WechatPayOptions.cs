namespace Study402Online.OrderService.Api.Application.Commands
{
    /// <summary>
    /// 微信支付配置
    /// </summary>
    public class WechatPayOptions
    {
        public string API_V3_Key;

        public string MerchantId { get; set; }

        public string AppId { get; set; }

        public Uri NotifyUri { get; set; }

        public string CertificateSerialNumber { get; set; }
    }
}