using MediatR;
using Study402Online.Common.Model;

namespace UserService.Api.Application.Commands;

/// <summary>
/// 登录命令
/// </summary>
public record LoginCommand : IRequest<Result<string>>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
}
