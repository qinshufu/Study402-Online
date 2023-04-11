using Microsoft.Extensions.DependencyInjection;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息加载器
/// </summary>
public class MessageLoader : IMessageLoader
{
    private readonly IServiceProvider _serviceProvider;

    static ConcurrentDictionary<string, Type> MessageTypesCache = new();

    static ConcurrentDictionary<string, Type> MessageHandlersCache = new();

    public MessageLoader(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessage LoadMessage(MessageLog log)
    {
        return (IMessage)JsonSerializer.Deserialize(log.Value, GetMessageType(log))!;
    }

    private Type GetMessageType(MessageLog log) => MessageTypesCache.GetOrAdd(log.Type, name => Type.GetType(name)!);

    public object LoadMessageHandler(MessageLog log)
    {
        var handlerType = GetMessageHandler(log);
        return _serviceProvider.GetRequiredService(handlerType);
    }

    private Type GetMessageHandler(MessageLog log) => MessageHandlersCache
        .GetOrAdd(log.Type, name => typeof(IMessageHandler<>).MakeGenericType(GetMessageType(log)));
}
