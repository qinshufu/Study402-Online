using Study402Online.BuildingBlocks.LocalMessage;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 渲染并保存预览页面消息
/// </summary>
public class PreviewPageRenderAndSaveMessage : IMessage
{
    /// <summary>
    /// 课程 ID
    /// </summary>
    public int CourseId { get; }

    public PreviewPageRenderAndSaveMessage(int id)
    {
        CourseId = id;
    }

}
