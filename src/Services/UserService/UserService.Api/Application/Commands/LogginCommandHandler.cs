using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Study402Online.Common.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Study402Online.UserService.Api.Configurations;

namespace Study402Online.UserService.Api.Application.Commands;

/// <summary>
/// 登录命令处理器
/// </summary>
public class LogginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly SignInManager<IdentityUser> _signinManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IOptions<JwtOptions> _options;

    public LogginCommandHandler(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signinManager,
        IOptions<JwtOptions> options)
    {
        _signinManager = signinManager;
        _userManager = userManager;
        _options = options;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _signinManager.PasswordSignInAsync(request.UserName, request.Password, false, true);

        if (result.IsLockedOut)
            return ResultFactory.Fail<string>("失败次数太多，帐号已经被锁");

        if (result.IsNotAllowed)
            return ResultFactory.Fail<string>("该账号被限制登录");

        if (result.Succeeded is false)
            return ResultFactory.Fail<string>("登录失败");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires = DateTime.UtcNow.Add(_options.Value.Expire),
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Credentials)), SecurityAlgorithms.HmacSha256Signature),
            Claims = new Dictionary<string, object>
            {
                ["user"] = request.UserName,
            }
        };

        var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

        return ResultFactory.Success(token);
    }
}
