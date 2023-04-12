using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Models.DataModels;

namespace UserService.Api.Instructure;

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