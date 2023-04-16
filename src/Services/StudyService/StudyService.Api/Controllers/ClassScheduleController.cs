using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.StudyService.Api.Application.Commands;

namespace Study402Online.StudyService.Api.Controllers;

/// <summary>
/// 课程表接口
/// <ul>
/// 对于前端，获取课程信息，判断课程类型
/// <li>如果是免费课程，则直接将课程加入课程表；</li>
/// <li>如果是收费课程，则选择课程，创建课程的订单,调用订单服务获取支付二维码，支付完成后，则课程将被回调通知加入课程表</li>
/// </ul>
/// </summary>
[Route("api/class-schedule")]
[ApiController]
public class ClassScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 将课程直接加入课程表，课程必须为免费课程
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("/select/{Course:int}")]
    public Task<UnitResult> AddCourseToSchedule([FromRoute] CourseToScheduleAddCommand command) =>
        _mediator.Send(command);

    /// <summary>
    /// 选择课程，课程需为收费课程，将创建购买收费课程的订单
    /// </summary>
    /// <param name="command"></param>
    /// <returns>返回购买课程的订单 ID</returns>
    public Task<Result<int>> SelectCourse([FromRoute] CourseSelectCommand command) => _mediator.Send(command);
}