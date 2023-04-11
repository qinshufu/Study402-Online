using CaptchaService.Api.Services;
using MediatR;
using Study402Online.Common.Model;

namespace CaptchaService.Api.Applications.Commands;

/// <summary>
/// 验证码验证有效性命令
/// </summary>
public class CaptchaVerifyHandler : IRequestHandler<CaptchaVerifyCommand, Result<bool>>
{
    private readonly ICacheService _cacheService;

    public CaptchaVerifyHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<bool>> Handle(CaptchaVerifyCommand request, CancellationToken cancellationToken)
    {
        var codeValue = await _cacheService.GetObjectAsync<string, string>("captcha:" + request.Key);
        return ResultFactory.Success(codeValue == request.Code);
    }
}
