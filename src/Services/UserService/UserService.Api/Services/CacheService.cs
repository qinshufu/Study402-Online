using StackExchange.Redis;
using System.Text.Json;

namespace Study402Online.UserService.Api.Services;

/// <summary>
/// 缓存服务的默认实现，使用 Redis
/// </summary>
public class CacheService : ICacheService
{
    private readonly ConnectionMultiplexer _redis;

    public CacheService(ConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<TValue> GetObjectAsync<TKey, TValue>(TKey key)
    {
        var @string = await _redis.GetDatabase().StringGetAsync(key!.ToString());
        return JsonSerializer.Deserialize<TValue>(@string!)!;
    }

    public async Task SetObjectAsync<TKey, TValue>(TKey key, TValue value)
    {
        await _redis.GetDatabase().StringSetAsync(key!.ToString(), JsonSerializer.Serialize(value));
    }

    public async Task SetObjectAsync<TKey, TValue>(TKey key, TValue value, TimeSpan valid)
    {
        await _redis.GetDatabase().StringSetAsync(key!.ToString(), JsonSerializer.Serialize(value), valid);
    }

    public async Task SetObjectAsync<TKey, TValue>(TKey key, TValue value, DateTime expire)
    {
        var validTime = expire - DateTime.Now;
        await _redis.GetDatabase().StringSetAsync(key!.ToString(), JsonSerializer.Serialize(value), validTime);
    }

    public async Task RemoveAsync<TKey>(TKey key)
    {
        await _redis.GetDatabase().KeyDeleteAsync(key!.ToString());
    }
}