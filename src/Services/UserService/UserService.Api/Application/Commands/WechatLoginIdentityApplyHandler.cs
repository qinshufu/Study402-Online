using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Study402Online.Common.Model;
using Study402Online.UserService.Api.Services;

namespace Study402Online.UserService.Api.Application.Commands;

/// <summary>
/// 微信登陆标识申请命令处理器
/// </summary>
public class WechatLoginIdentityApplyHandler : IRequestHandler<WechatLoginIdentityApplyCommand, Result<string>>
{
    private ICacheService _cacheService;

    public WechatLoginIdentityApplyHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<string>> Handle(WechatLoginIdentityApplyCommand request, CancellationToken cancellationToken)
    {
        var loginIdentity = Guid.NewGuid();
        await _cacheService.SetObjectAsync($"request:login:{loginIdentity}", DateTime.Now, TimeSpan.FromMinutes(30));

        return ResultFactory.Success(loginIdentity.ToString());
    }
}