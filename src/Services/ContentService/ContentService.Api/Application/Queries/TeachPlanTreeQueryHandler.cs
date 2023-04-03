using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Helpers;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 获取课程计划命令处理器
/// </summary>
public class TeachPlanTreeQueryHandler : IRequestHandler<TeachPlanTreeQuery, Result<List<TeachPlanTreeNode>>>
{
    private ContentDbContext _contentDbContext;

    public TeachPlanTreeQueryHandler(ContentDbContext contentDbContext) => _contentDbContext = contentDbContext;

    public async Task<Result<List<TeachPlanTreeNode>>> Handle(TeachPlanTreeQuery request, CancellationToken cancellationToken)
    {
        static TeachPlanTreeNode CreateCourseChapter(TeachPlan chapter, IEnumerable<TeachPlan> sections)
        {
            var result = new TeachPlanTreeNode();
            PocoHelper.CopyProperties(chapter, result);
            result.CourseSections = sections
                .Select(p => PocoHelper.Make<TeachPlanTreeNode>(p))
                .ToList();
            return result;
        }

        var plans = await _contentDbContext.TeachPlans.Where(p => p.CourseId == request.Course).ToArrayAsync();
        var planNodes = from chapter in plans.Where(p => p.Parent == 0)
                        join section in plans on chapter.Id equals section.Parent into sections
                        select CreateCourseChapter(chapter, sections);

        return ResultFactory.Success(planNodes.ToList());
    }
}
