namespace CaptchaService.Api.Models.ViewModels;

/// <summary>
/// 图片验证码
/// </summary>
public class PictureCaptcha
{
    /// <summary>
    /// Base64 编码的小型图片
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// key 用来唯一标识当前生成的验证码
    /// </summary>
    public Guid Key { get; set; }
}
