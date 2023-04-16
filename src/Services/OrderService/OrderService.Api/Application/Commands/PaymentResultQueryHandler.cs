using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Instructure;
using Study402Online.OrderService.Model.DateModels;

namespace Study402Online.OrderService.Api.Application.Commands;

/// <summary>
/// 支付结果查询处理器
/// </summary>
public class PaymentResultQueryHandler : IRequestHandler<PaymentResultQueryCommand, Result<OrderPayRecord>>
{
    private readonly OrderServiceDbContext _dbContext;

    public PaymentResultQueryHandler(OrderServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<OrderPayRecord>> Handle(
        PaymentResultQueryCommand request,
        CancellationToken cancellationToken)
    {
        var record = await _dbContext.OrderPayRecords.SingleOrDefaultAsync(r => r.PayNo == request.PayNo);

        if (record is null)
            return ResultFactory.Fail<OrderPayRecord>("支付记录不存在");

        return ResultFactory.Success(record);
    }
}