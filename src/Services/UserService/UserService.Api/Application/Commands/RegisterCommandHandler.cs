using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;

namespace Study402Online.UserService.Api.Application.Commands;

/// <summary>
/// 注册命令处理器
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(u => u.UserName == request.Name))
        {
            return ResultFactory.Fail<string>("同名账号已经存在");
        }

        var result = await _userManager.CreateAsync(new IdentityUser { UserName = request.Name }, request.Password);

        return result switch
        {
            null or { Succeeded: false } => ResultFactory.Fail<string>("账号注册失败"),
            _ => ResultFactory.Success(request.Name)
        };
    }
}
