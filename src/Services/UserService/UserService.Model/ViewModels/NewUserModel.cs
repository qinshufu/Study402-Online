namespace Study402Online.UserService.Model.ViewModels
{
    /// <summary>
    /// 创建新用户后的返回
    /// </summary>
    public class NewUserModel
    {
        /// <summary>
        /// 用户 ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码 （明文）
        /// </summary>
        public string Password { get; set; }
    }
}