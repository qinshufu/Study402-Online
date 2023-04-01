using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Helpers;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.RequestHandlers;

/// <summary>
/// 获取课程计划命令处理器
/// </summary>
public class GetTeachPlanTreeCommandHandler : IRequestHandler<GetTeachPlanTreeCommand, List<TeachPlanTreeNode>>
{
    private ContentDbContext _contentDbContext;

    public GetTeachPlanTreeCommandHandler(ContentDbContext contentDbContext) => _contentDbContext = contentDbContext;

    public async Task<List<TeachPlanTreeNode>> Handle(GetTeachPlanTreeCommand request, CancellationToken cancellationToken)
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

        return planNodes.ToList();
    }
}
