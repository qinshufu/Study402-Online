using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Instructure;
using Study402Online.OrderService.Model.DateModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 订单创建命令
/// </summary>
public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, Result<int>>
{
    private readonly OrderServiceDbContext _dbContext;

    private readonly IMapper _mapper;

    public OrderCreateHandler(OrderServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Orders.AnyAsync(o => o.ExternalBusinessId == request.ExternalBusinessId))
            return ResultFactory.Fail<int>("对应的订单已经存在，请勿重复创建");

        var order = _mapper.Map<Order>(request);
        var orderItems = _mapper.Map<IEnumerable<OrderItem>>(request.OrderItems);

        await _dbContext.AddAsync(order);
        await _dbContext.AddRangeAsync(orderItems);

        return ResultFactory.Success(order.Id);
    }
}