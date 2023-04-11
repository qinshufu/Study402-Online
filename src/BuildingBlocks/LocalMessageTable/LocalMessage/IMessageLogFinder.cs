using Study402Online.BuildingBlocks.LocalMessage.DataModels;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息日志查找器
/// </summary>
public interface IMessageLogFinder
{
    /// <summary>
    /// 查找需要处理的消息日志
    /// </summary>
    /// <param name="number">取出的消息日志数量</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<MessageLog>> FindAsync(int number, CancellationToken cancellationToken);
}
