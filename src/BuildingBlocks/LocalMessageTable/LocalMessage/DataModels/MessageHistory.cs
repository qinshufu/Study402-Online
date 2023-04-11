namespace Study402Online.BuildingBlocks.LocalMessage.DataModels;

/// <summary>
/// 消息历史
/// </summary>
/// <remarks>执行完成的消息将被移动到这张表，包括到达重试上线的消息</remarks>
public class MessageHistory
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 消息对象类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 消息的内容
    /// </summary>
    /// <remarks>序列化的消息将被存储在这里</remarks>
    public string Value { get; set; }

    /// <summary>
    /// 消息的上次处理时间
    /// </summary>
    public DateTime LastHandleTime { get; set; }

    /// <summary>
    /// 消息的重试次数
    /// </summary>
    public int RetriesNumber { get; set; }

    /// <summary>
    /// 消息状态
    /// </summary>
    public MessageLogStatus Status { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
