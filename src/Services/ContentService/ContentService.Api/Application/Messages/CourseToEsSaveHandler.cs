using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Api.Application.Services;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 保存课程到 ElasticSearch
/// </summary>
public class CourseToEsSaveHandler : IMessageHandler<CourseToEsSaveMessage>
{
    ISearchService SearchService { get; }

    public CourseToEsSaveHandler(ISearchService searchService)
    {
        SearchService = searchService;
    }

    public Task HandleAsync(CourseToEsSaveMessage message, CancellationToken cancellationToken = default) => SearchService.AddCourseDocumentAsync(message.Course);
}
