namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息处理器接口
/// </summary>
public interface IMessageHandler<TMessage>
{

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task HandleAsync(TMessage message, CancellationToken cancellationToken = default);
}
