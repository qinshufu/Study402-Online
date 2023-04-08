using Microsoft.CSharp.RuntimeBinder;
using StackExchange.Redis;
using Study402Online.Common.Model;
using System.Text.Json;

namespace Study402Online.ContentService.Api.Application.Services;


/// <summary>
/// 缓存服务
/// </summary>
public class CacheService : ICacheService
{
    private readonly ConnectionMultiplexer _redis;

    public CacheService(ConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    /// <summary>
    /// 缓存数据库行
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<UnitResult> CacheRowAsync<T>(T entity, string entityKey = default)
    {
        var name = entity.GetType().FullName;
        var id = default(object);

        try
        {
            id = entityKey ?? ((dynamic)entity).Id;
        }
        catch (RuntimeBinderException)
        {
            throw new InvalidOperationException("无效的操作，数据库行必须存在 ID 列作为唯一标识，或者手动指定 key");
        }

        var cacheKey = $"entity:{name}:{id}";
        var db = _redis.GetDatabase();

        var success = await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(entity));

        return success ? ResultFactory.Success() : ResultFactory.Fail("缓存失败");
    }
}
