namespace Study402Online.StudyService.Api.Models.ViewModels;

/// <summary>
/// 课程视频信息
/// </summary>
public class VideoInfo
{
    /// <summary>
    /// 视频 URL，用户可通过该链接访问视频
    /// </summary>
    public Uri Url { get; set; }

    /// <summary>
    /// 免费课程
    /// </summary>
    public bool IsFreeCourse { get; set; }

    /// <summary>
    /// 试学视频
    /// </summary>
    public bool IsTrial { get; set; }
}
