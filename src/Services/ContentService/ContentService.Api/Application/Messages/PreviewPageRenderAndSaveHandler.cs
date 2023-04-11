using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Api.Application.Services;
using Study402Online.ContentService.Api.Infrastructure;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 渲染并保存预览页面消息处理器
/// </summary>
public class PreviewPageRenderAndSaveHandler : IMessageHandler<PreviewPageRenderAndSaveMessage>
{
    ContentDbContext DbContext { get; }

    ITemplateService TemplateService { get; }

    IMediaService MediaService { get; }

    public PreviewPageRenderAndSaveHandler(ContentDbContext dbContext, ITemplateService templateService, IMediaService mediaService)
    {
        DbContext = dbContext;
        TemplateService = templateService;
        MediaService = mediaService;
    }


    public async Task HandleAsync(PreviewPageRenderAndSaveMessage message, CancellationToken cancellationToken = default)
    {
        var course = await DbContext.Courses.SingleAsync(c => c.Id == message.CourseId);
        var previewPageResult = await TemplateService.RenderAsync("Course", course);

        if (previewPageResult is { Success: true })
            _ = await MediaService.UploadFileAsync($"/preview/course/{course.Id}", previewPageResult.Value);

        previewPageResult.Value.Close();
    }
}
