using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Api.Application.Services;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 课程缓存消息处理器
/// </summary>
public class CourseCacheHandler : IMessageHandler<CourseCacheMessage>
{
    ICacheService CacheService { get; }

    public CourseCacheHandler(ICacheService cacheService)
    {
        CacheService = cacheService;
    }


    public Task HandleAsync(CourseCacheMessage message, CancellationToken cancellationToken = default) =>
        CacheService.CacheRowAsync(message.Course);
}
