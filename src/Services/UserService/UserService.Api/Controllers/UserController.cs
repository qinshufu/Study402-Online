using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using UserService.Api.Application.Commands;

namespace UserService.Api.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public Task<Result<string>> LoginAsync([FromBody] LoginCommand command) => _mediator.Send(command);

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public Task<Result<string>> RegisterAsync([FromBody] RegisterCommand command) => _mediator.Send(command);
}
