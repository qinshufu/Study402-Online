using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;

namespace SearchService.Api.Controllers;

/// <summary>
/// 课程搜索
/// </summary>
[ApiController]
[Route("/api/course-search")]
public class CourseSearchController : ControllerBase
{
    /// <summary>
    /// 搜索课程
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public Task<Result<Guid>> SearchCourseAsync([FromQuery] string[] keywords)
    {
        return Task.FromResult(ResultFactory.Success(Guid.NewGuid()));
    }

    /// <summary>
    /// 插入课程文档
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    [HttpPost("add-document")]
    public Task<UnitResult> AddCourseAsync([FromBody] Course course)
    {
        return Task.FromResult(ResultFactory.Success());
    }
}
