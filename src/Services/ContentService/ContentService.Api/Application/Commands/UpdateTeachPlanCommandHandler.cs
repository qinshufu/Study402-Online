
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Helpers;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 更行课程计划命令
/// </summary>
public class UpdateTeachPlanCommandHandler : IRequestHandler<UpdateTeachPlanCommand, Result<TeachPlan>>
{
    private readonly ContentDbContext _context;

    public UpdateTeachPlanCommandHandler(ContentDbContext context) => _context = context;

    public async Task<Result<TeachPlan>> Handle(UpdateTeachPlanCommand request, CancellationToken cancellationToken)
    {
        var teachPlan = await _context.TeachPlans.SingleOrDefaultAsync(p => p.Id == request.Model.Id);
        if (teachPlan is null)
            return ResultFactory.Fail<TeachPlan>("指定课程计划不存在: Id = " + request.Model.Id);

        PocoHelper.CopyProperties(request.Model, teachPlan);

        return ResultFactory.Success(teachPlan);
    }
}
