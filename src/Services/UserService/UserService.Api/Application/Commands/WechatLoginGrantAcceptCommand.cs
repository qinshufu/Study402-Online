using MediatR;
using Microsoft.AspNetCore.Identity;
using Study402Online.Common.Model;
using UserService.Api.Models.ViewModels;

namespace UserService.Api.Application.Commands;

/// <summary>
/// 接受微信登录授权命令
/// </summary>
public record WechatLoginGrantAcceptCommand : IRequest<Result<NewUserModel>>
{
    /// <summary>
    /// 授权码
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// 登录状态（标识）
    /// </summary>
    public string State { get; private set; }
}