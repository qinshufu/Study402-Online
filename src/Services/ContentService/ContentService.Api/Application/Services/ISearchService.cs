using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 搜索服务接口
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// 搜索课程
    /// </summary>
    /// <param name="keywords"></param>
    /// <returns></returns>
    Task<Result<Guid>> SearchCourseAsync(string[] keywords);

    /// <summary>
    /// 添加课程文档
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    Task<UnitResult> AddCourseDocumentAsync(Course course);
}
