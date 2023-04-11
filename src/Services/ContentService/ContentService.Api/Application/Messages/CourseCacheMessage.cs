using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 课程缓存消息
/// </summary>
public class CourseCacheMessage : IMessage
{
    /// <summary>
    /// 课程信息
    /// </summary>
    public Course Course { get; }

    public CourseCacheMessage(Course course)
    {
        Course = course;
    }

}
