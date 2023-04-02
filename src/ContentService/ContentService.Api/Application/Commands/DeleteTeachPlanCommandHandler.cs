using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.ContentService.Api.Infrastructure;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 删除指定课程计划命令
/// </summary>
public class DeleteTeachPlanCommandHandler : IRequestHandler<DeleteTeachPlanCommand, int>
{
    private readonly ContentDbContext _context;

    public DeleteTeachPlanCommandHandler(ContentDbContext context) => _context = context;

    public async Task<int> Handle(DeleteTeachPlanCommand request, CancellationToken cancellationToken)
    {
        var teachPlan = await _context.TeachPlans.SingleOrDefaultAsync(p => p.Id == request.TeachPlan);
        if (teachPlan is null)
            throw new InvalidOperationException("指定课程计划不存在: Id = " + request.TeachPlan);

        var sectionsOfThis = _context.TeachPlans.Where(p => p.Parent == teachPlan.Id);

        await _context.TeachPlanMedias
            .Where(p => sectionsOfThis.Select(section => section.Id).Contains(p.Course))
            .ExecuteDeleteAsync(cancellationToken);
        await _context.TeachPlans.Where(p => p.Parent == teachPlan.Id).ExecuteDeleteAsync(cancellationToken);
        await _context.TeachPlans.Where(p => p.Id == teachPlan.Id).ExecuteDeleteAsync(cancellationToken);

        return teachPlan.Id;
    }
}
