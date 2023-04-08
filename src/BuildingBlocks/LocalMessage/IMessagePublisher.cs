namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 发布消息接口
/// </summary>
public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;
}
