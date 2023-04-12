using Refit;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.StudyService.Api.HttpClients;

/// <summary>
/// 课程服务接口
/// </summary>
public interface ICourseServiceClient
{
    /// <summary>
    /// 获取课程信息
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    [Get("/api/course/get")]
    Task<Result<CourseInformationModel>> GetCourseInfoAsync([AliasAs("id")] int course);
}
