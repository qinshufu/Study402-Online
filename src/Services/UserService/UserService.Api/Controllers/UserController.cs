using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.UserService.Api.Application.Commands;
using Study402Online.UserService.Model.ViewModels;

namespace Study402Online.UserService.Api.Controllers;

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

    /// <summary>
    /// 申请一个标识用户第三方登录请求的 id，参见 https://developers.weixin.qq.com/doc/oplatform/Website_App/WeChat_Login/Wechat_Login.html
    /// </summary>
    /// <returns></returns>
    [HttpPost("apply-for-login-identity")]
    public Task<Result<string>> ApplyForWechatLoginIdentity() => _mediator.Send(new WechatLoginIdentityApplyCommand());

    /// <summary>
    /// 根据用户授权创建本地用户 redirect_uri?code=CODE&state=STATE
    /// </summary>
    /// <returns></returns>
    [HttpPost("accept-wechat-grant")]
    public Task<Result<NewUserModel>> AcceptWechatLoginGrant([FromQuery] WechatLoginGrantAcceptCommand command) =>
        _mediator.Send(command);
}