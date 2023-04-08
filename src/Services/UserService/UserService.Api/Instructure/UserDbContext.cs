using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserService.Api.Instructure;

/// <summary>
/// 用户数据库上下文
/// </summary>
public class UserDbContext : IdentityDbContext
{

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    { }
}
