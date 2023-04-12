using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.StudyService.Api.Application.Commands;

namespace Study402Online.StudyService.Api.Controllers;

/// <summary>
/// 课程表接口
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
    public Task<UnitResult> AddCourseToSchedule([FromRoute] CourseToScheduleAddCommand command) => _mediator.Send(command);
}
