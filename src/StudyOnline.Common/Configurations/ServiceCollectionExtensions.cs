using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Study402Online.Common.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerGenDefault(
            this IServiceCollection services,
            Action<SwaggerGenOptions>? setupAction = null)
        {
            var xmlname = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var path = Path.Combine(AppContext.BaseDirectory, xmlname);

            services.AddSwaggerGen(opts => opts.IncludeXmlComments(path, true));
            services.AddSwaggerGen(setupAction);

            return services;
        }
    }
}
