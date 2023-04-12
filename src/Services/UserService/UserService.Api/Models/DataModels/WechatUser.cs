using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserService.Api.Models.DataModels;

/// <summary>
/// 网站用户
/// </summary>
public class WechatUser
{
    /// <summary>
    /// 当前网站的用户 ID
    /// </summary>
    [Key]
    public required string LocalId { get; set; }

    public string? OpenId { get; set; }

    public string? UnionId { get; set; }
}