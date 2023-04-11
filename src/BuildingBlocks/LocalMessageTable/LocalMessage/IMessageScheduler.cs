using Study402Online.BuildingBlocks.LocalMessage.DataModels;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息调度接口
/// </summary>
public interface IMessageScheduler
{
    /// <summary>
    /// 调度消息
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ScheduleAsync(MessageLog messageLog, CancellationToken cancellationToken);
}
