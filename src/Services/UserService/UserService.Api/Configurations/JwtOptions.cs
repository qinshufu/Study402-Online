namespace Study402Online.UserService.Api.Configurations;

/// <summary>
/// JWT 配置
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// token的有效时间
    /// </summary>
    public TimeSpan Expire { get; set; }

    /// <summary>
    /// 手中
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 颁发者
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 证书
    /// </summary>
    public string Credentials { get; set; }
}
