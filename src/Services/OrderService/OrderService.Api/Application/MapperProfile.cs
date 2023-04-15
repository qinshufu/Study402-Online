using AutoMapper;
using Study402Online.OrderService.Api.Application.Commands;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;

namespace Study402Online.OrderService.Api.Application;

/// <summary>
/// 映射配置
/// </summary>
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<OrderCreateCommand, Order>();
        CreateMap<OrderItemModel, OrderItem>();
    }
}
