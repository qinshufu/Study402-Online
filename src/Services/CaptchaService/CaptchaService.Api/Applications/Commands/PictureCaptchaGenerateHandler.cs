using CaptchaService.Api.Models.ViewModels;
using CaptchaService.Api.Services;
using Hei.Captcha;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Study402Online.Common.Model;

namespace CaptchaService.Api.Applications.Commands;

/// <summary>
/// 生成图形验证码命令处理器
/// </summary>
public class PictureCaptchaGenerateHandler : IRequestHandler<PictureCaptchaGenerateCommand, Result<PictureCaptcha>>
{

    private readonly ICacheService _cacheService;

    private readonly SecurityCodeHelper _codeHelper;

    public PictureCaptchaGenerateHandler(ICacheService cacheService, SecurityCodeHelper codeHelper)
    {
        _cacheService = cacheService;
        _codeHelper = codeHelper;
    }

    public async Task<Result<PictureCaptcha>> Handle(PictureCaptchaGenerateCommand request, CancellationToken cancellationToken)
    {
        var codeKey = Guid.NewGuid();
        var code = _codeHelper.GetRandomEnDigitalText(6);
        var picture = _codeHelper.GetEnDigitalCodeByte(code);

        await _cacheService.SetObjectAsync("captcha:" + codeKey, code);

        return ResultFactory.Success(new PictureCaptcha
        {
            Key = codeKey,
            Picture = Base64UrlTextEncoder.Encode(picture)
        });
    }
}
