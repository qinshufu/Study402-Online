using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.UserService.Api.Application.Commands;

/// <summary>
/// 注册命令
/// </summary>
public record RegisterCommand : IRequest<Result<string>>
{
    /// <summary>
    /// 账号名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
}
