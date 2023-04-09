using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    public static WebApplication AddConsul(this WebApplication application, Action<ConsulClient> action)
    {
        var client = application.Services.GetRequiredService<ConsulClient>();
        action(client);

        return application;
    }

    /// <summary>
    /// 添加 Consul 并应用默认配置方法
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseDefaultConsul(this WebApplication application)
        =>
        application.AddConsul(consul =>
        {
            var options = application.Services.GetRequiredService<IOptions<ConsulOptions>>().Value;

            consul.Agent.ServiceRegister(new Consul.AgentServiceRegistration
            {
                ID = options.ServiceId,
                Name = options.ServiceName,
                Address = options.ServiceAddress,
                Port = options.ServicePort,
                Check = new Consul.AgentServiceCheck
                {
                    TTL = TimeSpan.FromSeconds(options.TTL)
                }
            });
        });
}
