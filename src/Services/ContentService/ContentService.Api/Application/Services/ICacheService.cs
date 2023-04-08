using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 缓存服务
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 缓存数据行
    /// </summary>
    /// <returns></returns>
    Task<UnitResult> CacheRowAsync<T>(T entity, string entityKey = default);
}
