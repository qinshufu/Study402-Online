namespace UserService.Api.Configurations
{
    /// <summary>
    /// 微信登录选项
    /// </summary>
    public class WechatOptions
    {
        /// <summary>
        /// 应用 ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Secret { get; set; }
    }
}