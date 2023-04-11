using MediatR;
using Study402Online.Common.Model;

namespace CaptchaService.Api.Applications.Commands;

/// <summary>
/// 验证码验证命令
/// </summary>
public record CaptchaVerifyCommand : IRequest<Result<bool>>
{
    /// <summary>
    /// 验证码的 key
    /// </summary>
    public Guid Key { get; set; }

    /// <summary>
    /// 验证码的值
    /// </summary>
    public string Code { get; set; }
}
