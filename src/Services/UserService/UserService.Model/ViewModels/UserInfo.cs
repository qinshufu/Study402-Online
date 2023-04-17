using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study402Online.UserService.Model.ViewModels;

/// <summary>
/// 用户相关信息
/// </summary>
public class UserInfo
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
}
