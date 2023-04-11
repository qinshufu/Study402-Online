using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 保存课程到 ElasticSearch 消息
/// </summary>
public class CourseToEsSaveMessage : IMessage
{
    public CourseToEsSaveMessage(Course course)
    {
        Course = course;
    }

    public Course Course { get; }
}
