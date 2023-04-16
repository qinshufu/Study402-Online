using System.Text.Json;
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

    /// <summary>
    /// 查询交易状态（或者说支付状态）
    /// </summary>
    /// <param name="payNo">支付流水号</param>
    /// <param name="merchatId">商户 ID</param>
    /// <returns></returns>
    [Get("/v3/pay/transactions/out-trade-no/{transactionId:string}")]
    Task<JsonElement> QueryOrderStatusAsync(string payNo, string merchatId);
}