using MassTransit.MessageData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Helpers;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 教学计划获取命令处理器
/// </summary>
public class TeachPlanQueryHandler : IRequestHandler<TeachPlanQuery, Result<TeachPlanInfo>>
{
    private readonly ContentDbContext _dbContext;

    public TeachPlanQueryHandler(ContentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TeachPlanInfo>> Handle(TeachPlanQuery request, CancellationToken cancellationToken)
    {
        var teachPlan = await _dbContext.TeachPlans.SingleOrDefaultAsync(p => p.Id == request.TeachPlanId);

        if (teachPlan == null)
            return ResultFactory.Fail<TeachPlanInfo>("指定的教学计划不存在");

        var media = await _dbContext.TeachPlanMedias.SingleOrDefaultAsync(m => m.TeachPlan == request.TeachPlanId);

        var info = new TeachPlanInfo()
        {
            Id = teachPlan.Id,
            CourseId = teachPlan.CourseId,
            MediaId = media?.MediaId,
            Description = teachPlan.Description,
            Duration = teachPlan.Duration,
            EndTime = teachPlan.EndTime,
            Level = teachPlan.Level,
            MediaName = media?.Name,
            Name = teachPlan.Name,
            OrderBy = teachPlan.OrderBy,
            Parent = teachPlan.Parent,
            Preview = teachPlan.Preview,
            StartTime = teachPlan.StartTime,
            Type = teachPlan.Type
        };

        return ResultFactory.Success(info);

    }
}
