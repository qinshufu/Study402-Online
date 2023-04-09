using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Study402Online.Common.BackgroundServices;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Study402Online.Common.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerGenDefault(
            this IServiceCollection services,
            Action<SwaggerGenOptions>? setupAction = null)
        {
            var xmlname = Assembly.GetEntryAssembly().GetName().Name + ".xml";
            var path = Path.Combine(AppContext.BaseDirectory, xmlname);

            services.AddSwaggerGen(opts =>
            {
                opts.IncludeXmlComments(path, true);
                opts.CustomOperationIds(options => options.ActionDescriptor.Id);
                opts.CustomOperationIds(opts => Regex.Match(opts.ActionDescriptor.DisplayName!, @"(\w+\.)+(?<method>\w+)").Groups["method"].Value);
                //opts.OperationFilter<JsonContentTypeOperationFilter>();
            });
            services.AddSwaggerGen(setupAction);

            return services;
        }

        /// <summary>
        /// 添加 consul
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            services.AddHostedService<ConsulTTLBackgroundService>();

            services.AddSingleton(ctx =>
            {
                var consul = new ConsulClient(new ConsulClientConfiguration
                {
                    Address = ctx.GetRequiredService<IOptions<ConsulOptions>>().Value.ConsulUri,
                });

                return consul;
            });
            return services;
        }
    }
}
