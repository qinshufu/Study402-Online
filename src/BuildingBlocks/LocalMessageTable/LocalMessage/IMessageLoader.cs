using Study402Online.BuildingBlocks.LocalMessage.DataModels;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息加载器
/// </summary>
public interface IMessageLoader
{
    /// <summary>
    /// 从消息日志中加载消息
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    IMessage LoadMessage(MessageLog log);

    /// <summary>
    /// 从消息日志中加载消息处理器
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    object LoadMessageHandler(MessageLog log);
}
