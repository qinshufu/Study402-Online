using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 选择课程命令
/// </summary>
public record CourseToScheduleAddCommand : IRequest<UnitResult>
{
    /// <summary>
    /// 课程 ID
    /// </summary>
    public int Course { get; set; }
}
