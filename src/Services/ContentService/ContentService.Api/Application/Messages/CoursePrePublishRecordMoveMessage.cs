using Study402Online.BuildingBlocks.LocalMessage;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 移动课程预发布发布记录消息
/// </summary>
public class CoursePrePublishRecordMoveMessage : IMessage
{
    /// <summary>
    /// 记录 ID
    /// </summary>
    public int RecordId { get; }

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="id">课程预发布记录 ID</param>
    public CoursePrePublishRecordMoveMessage(int id)
    {
        RecordId = id;
    }

}
