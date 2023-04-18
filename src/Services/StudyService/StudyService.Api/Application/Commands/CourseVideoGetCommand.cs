using MediatR;
using Study402Online.Common.Model;
using Study402Online.StudyService.Api.Models.ViewModels;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 课程视频获取命令
/// </summary>
public class CourseVideoGetCommand : IRequest<Result<VideoInfo>>
{
    /// <summary>
    /// 课程 ID
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// 学习计划 ID
    /// </summary>
    public int TeachPlanId { get; set; }

    /// <summary>
    /// 媒体 ID
    /// </summary>
    public int MediaId { get; set; }
}
