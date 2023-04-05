using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 绑定媒资文件命令处理器
/// </summary>
public class BindMediaCommandHandler : IRequestHandler<BindMediaCommand, UnitResult>
{
    private readonly ContentDbContext _context;

    public BindMediaCommandHandler(ContentDbContext context) => _context = context;

    public async Task<UnitResult> Handle(BindMediaCommand request, CancellationToken cancellationToken)
    {
        var oldTeachPlan = await _context.TeachPlanMedias
            .Where(b => b.MediaId == request.MediaFile)
            .SingleOrDefaultAsync(cancellationToken);
        if (oldTeachPlan is null)
            return ResultFactory.Fail("课程计划不存在");

        await _context.TeachPlanMedias.Where(b => b.MediaId == request.MediaFile).ExecuteDeleteAsync(cancellationToken);

        await _context.AddAsync(new TeachPlanMedia
        {
            MediaId = request.MediaFile,
            Course = oldTeachPlan.Course,
            Name = oldTeachPlan.Name,
            TeachPlan = oldTeachPlan.Id,
            // ... 其他属性的设置
        });

        return ResultFactory.Success();
    }
}