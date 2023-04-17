using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Study402Online.Common.Model;
using Study402Online.UserService.Api.Configurations;
using Study402Online.UserService.Api.Instructure;
using Study402Online.UserService.Model.ViewModels;
using Study402Online.UserService.Api.Services;
using Study402Online.UserService.Model.DataModels;

namespace Study402Online.UserService.Api.Application.Commands;

/// <summary>
/// 接受微信登录授权处理器，当用户成功授权以后，创建新的账户并将该账户与其微信账户关联起来
/// </summary>
public class WechatLoginGrantAcceptHandler : IRequestHandler<WechatLoginGrantAcceptCommand, Result<NewUserModel>>
{
    const string AccessTokenRequestUri =
        @"https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code
            ";

    private const string UserInfoUri =
        @"https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}";

    private readonly IOptions<WechatOptions> _options;
    private readonly ICacheService _cacheService;
    private readonly IHttpClientFactory _clientFactory;
    private readonly UserDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public WechatLoginGrantAcceptHandler(
        IOptions<WechatOptions> options,
        ICacheService cacheService,
        IHttpClientFactory clientFactory,
        UserDbContext dbContext,
        UserManager<IdentityUser> userManager)
    {
        _options = options;
        _cacheService = cacheService;
        _clientFactory = clientFactory;
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<Result<NewUserModel>> Handle(WechatLoginGrantAcceptCommand request,
        CancellationToken cancellationToken)
    {
        var client = _clientFactory.CreateClient("Wechat");

        var loginIdentity = await _cacheService.GetObjectAsync<string, string>($"request:login:{request.State}");

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (loginIdentity is null)
            return ResultFactory.Fail<NewUserModel>("无效的 State，或许登录已经过期");

        // 删除对应的 login identity 使之后的授权无效
        await _cacheService.RemoveAsync($"request:login:{request.State}");

        var requestUri =
            string.Format(AccessTokenRequestUri, _options.Value.AppId, _options.Value.Secret, request.Code);

        var loginResult = await client.GetFromJsonAsync<Dictionary<string, string>>(requestUri, cancellationToken);

        // 接口返回值示例
        /*
         当请求成功
           { 
            "access_token":"ACCESS_TOKEN", 
            "expires_in":7200, 
            "refresh_token":"REFRESH_TOKEN",
            "openid":"OPENID", 
            "scope":"SCOPE",
            "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            }
            当请求失败
            {"errcode":40029,"errmsg":"invalid code"}
         */

        if (loginResult!.ContainsKey("errcode"))
        {
            return ResultFactory.Fail<NewUserModel>("获取微信访问令牌失败，错误消息为: " +
                                                    loginResult.GetValueOrDefault("errmsg", "invalid code"));
        }

        // 这里存储一下微信的授权，直接存到 Redis 中，因为看起来这个不是需要持久化记录的东西，即使都是也不在意
        await _cacheService.SetObjectAsync($"login:wechat:{loginResult["openid"]}", loginResult, TimeSpan.FromHours(3));

        // 获取用户个人信息
        var userResult =
            await client.GetFromJsonAsync<Dictionary<string, string>>(
                string.Format(UserInfoUri, loginResult["access_token"], loginResult["openid"]));

        /*
         * 正确的 json 返回结果
         {
            "openid":"OPENID",
            "nickname":"NICKNAME",
            "sex":1,
            "province":"PROVINCE",
            "city":"CITY",
            "country":"COUNTRY",
            "headimgurl": "https://thirdwx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0",
            "privilege":[
            "PRIVILEGE1",
            "PRIVILEGE2"
            ],
            "unionid": " o6_bmasdasdsad6_2sgVt7hMZOPfL"
            }
         */

        if (userResult is null)
            return ResultFactory.Fail<NewUserModel>("获取微信用户信息失败");

        // 获取用户信息以后做一些其他的保存处理，这里啥也不干，就创建个新用户
        var user = new IdentityUser(userResult["nickname"])
        {
            NormalizedUserName = userResult["openid"]
        };
        var createResult = await _userManager.CreateAsync(user, "12345678");

        if (createResult.Succeeded is false)
            return ResultFactory.Fail<NewUserModel>("创建新用户失败");

        var userId = await _userManager.GetUserIdAsync(user);

        await _dbContext.WechatUsers.AddAsync(new WechatUser()
        {
            LocalId = userId,
            OpenId = userResult["openid"],
            UnionId = userResult["unionid"]
        });

        return ResultFactory.Success(
            new NewUserModel()
            {
                Id = userId,
                NickName = user.UserName!,
                Name = user.NormalizedUserName,
                Password = "12345678"
            });
    }
}