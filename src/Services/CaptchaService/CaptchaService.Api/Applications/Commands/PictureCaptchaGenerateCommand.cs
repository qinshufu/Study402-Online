using CaptchaService.Api.Models.ViewModels;
using MediatR;
using Study402Online.Common.Model;

namespace CaptchaService.Api.Applications.Commands;

/// <summary>
/// 生成图形验证码命令
/// </summary>
public record PictureCaptchaGenerateCommand : IRequest<Result<PictureCaptcha>>
{
}
