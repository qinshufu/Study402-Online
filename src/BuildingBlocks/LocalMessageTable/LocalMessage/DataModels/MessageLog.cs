using System.Text.Json;

namespace Study402Online.BuildingBlocks.LocalMessage.DataModels;

/// <summary>
/// 消息
/// </summary>
public class MessageLog
{

    public MessageLog() { }

    public MessageLog(IMessage message)
    {
        Type = message.GetType().AssemblyQualifiedName!;
        Value = JsonSerializer.Serialize(message, message.GetType());
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 消息的类型，这里存储的应该是类型的全名，将会根据这个名字获取 Type 对象
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
