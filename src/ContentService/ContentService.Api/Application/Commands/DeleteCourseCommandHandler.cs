using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 删除课程命令处理器
/// </summary>
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<object>>
{
    private readonly ContentDbContext _context;

    public DeleteCourseCommandHandler(ContentDbContext context) => _context = context;

    public async Task<Result<object>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        await _context.TeachPlanMedias.Where(m => m.Course == request.Course).ExecuteDeleteAsync(cancellationToken);
        await _context.TeachPlans.Where(m => m.CourseId == request.Course).ExecuteDeleteAsync(cancellationToken);
        await _context.Courses.Where(c => c.Id == request.Course).ExecuteDeleteAsync(cancellationToken);

        return ResultFactory.Success(default(object));
    }
}
