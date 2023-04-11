using Study402Online.BuildingBlocks.LocalMessage.DataModels;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息日志的处理器
/// </summary>
internal interface IMessageLogHandler
{
    Task HandleAsync(MessageLog log, CancellationToken cancellationToken = default);
}
