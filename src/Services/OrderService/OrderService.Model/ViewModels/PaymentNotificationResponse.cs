using System.Text.Json.Serialization;

namespace Study402Online.OrderService.Model.ViewModels;

/// <summary>
/// 支付通知的相应
/// </summary>
public class PaymentNotificationResponse
{
    public PaymentNotificationResponse(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public const string SuccessCode = "SUCCESS";

    /// <summary>
    /// 除了 SUCCESS 意外，其他皆为失败
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }
}