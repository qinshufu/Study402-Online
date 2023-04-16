using Refit;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Application.Commands;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.StudyService.Api.HttpClients;

/// <summary>
/// 订单服务
/// </summary>
public interface IOrderServiceClient
{
    /// <summary>
    /// 创建订单
    /// </summary>
    /// <returns></returns>
    [Post("/api/order/create")]
    Task<Result<int>> CreateOrderAsync([Body] OrderCreateCommand command);

    /// <summary>
    /// 支付订单，将放回支付信息
    /// </summary>
    /// <returns></returns>
    [Post("/api/order/pay")]
    Task<Result<PaymentModel>> PayOrderAsync([Body] OrderPayCommand command);
}