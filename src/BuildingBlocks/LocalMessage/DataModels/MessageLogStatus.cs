namespace Study402Online.BuildingBlocks.LocalMessage.DataModels;

/// <summary>
/// 消息执行状态
/// </summary>
public enum MessageLogStatus
{
    /// <summary>
    /// 活动状态
    /// </summary>
    /// <remarks>说明消息可以被执行</remarks>
    Created,

    /// <summary>
    /// 正在被处理
    /// </summary>
    Handling,

    /// <summary>
    /// 消息执行失败
    /// </summary>
    Failed,

    /// <summary>
    /// 消息执行成功
    /// </summary>
    Successful
}