using Refit;

namespace Study402Online.OrderService.Api.Application.Clients;

/// <summary>
/// 微信支付接口请求客户端，使用 JSAPI ，详见 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml
/// </summary>
public interface IWechatPayApiClient
{
    /// <summary>
    /// 创建订单 /v3/pay/transactions/jsapi
    /// </summary>
    /// <returns>预支付交易会话标识</returns>
    [Post("/v3/pay/transactions/jsapi")]
    Task<string> CreateOrderAsync(object order);
}