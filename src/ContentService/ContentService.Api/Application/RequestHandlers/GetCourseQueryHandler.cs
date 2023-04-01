using MediatR;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.Study402Online.ContentService.Model.ViewModel;
using Study402Online.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Study402Online.ContentService.Api.Application.RequestHandlers;

/// <summary>
/// 获取课程信息查询
/// </summary>
public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseInformationModel>
{
    private readonly ContentDbContext _context;

    public GetCourseQueryHandler(ContentDbContext context) => _context = context;

    public async Task<CourseInformationModel> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.SingleAsync(c => c.Id == request.Id);
        var courseMarket = await _context.CourseMarkets.SingleAsync(c => c.Id == request.Id);

        var result = new CourseInformationModel();
        PocoHelper.CopyProperties(courseMarket, result);
        PocoHelper.CopyProperties(course, result);

        return result;
    }
}