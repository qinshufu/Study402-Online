using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.StudyService.Api.Application.Commands;
using Study402Online.StudyService.Api.Models.ViewModels;

namespace Study402Online.StudyService.Api.Controllers;

/// <summary>
/// 课程相关
/// </summary>
[Route("/api/course")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 获取课程视频
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("video")]
    public Task<Result<VideoInfo>> GetVideo([FromQuery] CourseVideoGetCommand command) => _mediator.Send(command);
}
