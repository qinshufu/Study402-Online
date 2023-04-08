using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Study402Online.BuildingBlocks.LocalMessage;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddLocalMessage(this ServiceCollection services)
    {
        services.AddSingleton<IMessageScheduler, MessageScheduler>();
        services.AddOptions<LocalMessageOption>("LocalMessage");
        //services.AddHostedService<MessageScheduleService>();
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        services.AddScoped<IMessageLogHandler, MessageLogHandler>();
        services.AddScoped<IMessageLogFinder, MessageLogFinder>();

        return services;
    }

    public static ServiceCollection AddLocalMessageHandlers(this ServiceCollection services, params Assembly[] assemblies)
    {
        var handlers = assemblies.Select(a => a.ExportedTypes).SelectMany(types => types).Where(type => type.IsSubclassOf(typeof(IMessageHandler<>)));

        foreach (var handler in handlers)
        {
            var serviceType = handler.GetInterface(typeof(IMessageHandler<>).FullName!);

            if (serviceType is null)
                continue;

            // TODO 需要检查 serviceType 是否为 IMessageHandler<> 的泛型

            services.AddScoped(serviceType, handler);
        }

        return services;
    }
}
