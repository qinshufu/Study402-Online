using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Study402Online.Common.BackgroundServices;

namespace Study402Online.Common.Configurations;

/// <summary>
/// 应用扩展
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// 添加 Consul
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseConsul(this WebApplication application, Action<ConsulClient> action)
    {
        var client = application.Services.GetRequiredService<ConsulClient>();
        action(client);

        return application;
    }
}
