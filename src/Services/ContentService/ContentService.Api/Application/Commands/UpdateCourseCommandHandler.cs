using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Helpers;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 更新课程处理器
/// </summary>
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<CourseInformationModel>>
{
    private readonly ContentDbContext _context;

    public UpdateCourseCommandHandler(ContentDbContext context) => _context = context;

    public async Task<Result<CourseInformationModel>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.SingleAsync(c => c.Id == request.Model.Id);
        var market = await _context.CourseMarkets.SingleAsync(c => c.Id == request.Model.Id);

        PocoHelper.CopyProperties(request.Model, course);
        PocoHelper.CopyProperties(request.Model, market);

        _context.Update(course);
        _context.Update(market);

        await _context.SaveChangesAsync();

        var result = new CourseInformationModel();
        PocoHelper.CopyProperties(course, result);
        PocoHelper.CopyProperties(market, result);

        return ResultFactory.Success(result);
    }
}
