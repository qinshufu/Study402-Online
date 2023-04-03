using MediatR;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.ViewModel;
using Study402Online.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 获取课程信息查询
/// </summary>
public class CourseQueryHandler : IRequestHandler<CourseQuery, Result<CourseInformationModel>>
{
    private readonly ContentDbContext _context;

    public CourseQueryHandler(ContentDbContext context) => _context = context;

    public async Task<Result<CourseInformationModel>> Handle(CourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.SingleAsync(c => c.Id == request.Id);
        var courseMarket = await _context.CourseMarkets.SingleAsync(c => c.Id == request.Id);

        var result = new CourseInformationModel();
        PocoHelper.CopyProperties(courseMarket, result);
        PocoHelper.CopyProperties(course, result);

        return ResultFactory.Success(result);
    }
}