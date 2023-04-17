using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Study402Online.UserService.Model.DataModels;

namespace Study402Online.UserService.Api.Instructure;

/// <summary>
/// 用户数据库上下文
/// </summary>
public class UserDbContext : IdentityDbContext
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public DbSet<WechatUser> WechatUsers { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
}