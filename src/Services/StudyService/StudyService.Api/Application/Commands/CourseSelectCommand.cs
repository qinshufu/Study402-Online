using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 课程选择命令，将创建收费课程的订单
/// </summary>
public class CourseSelectCommand : IRequest<Result<int>>
{
    /// <summary>
    /// 课程 ID
    /// </summary>
    public int Course { get; set; }
}