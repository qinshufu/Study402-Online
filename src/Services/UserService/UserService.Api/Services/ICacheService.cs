namespace Study402Online.UserService.Api.Services;

/// <summary>
/// 缓存服务
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 设置对象到缓存中，缓存对象默认使用 JSON 序列化
    /// </summary>
    /// <typeparam name="TKey">缓存的键类型，将取其 ToString 的值</typeparam>
    /// <typeparam name="TValue">缓存对象类型</typeparam>
    /// <param name="key">缓存 key</param>
    /// <param name="value">缓存对象</param>
    /// <param name="valid">有效时间</param>
    /// <returns></returns>
    Task SetObjectAsync<TKey, TValue>(TKey key, TValue value, TimeSpan valid);

    /// <summary>
    /// 获取缓存的对象
    /// </summary>
    /// <typeparam name="TKey">缓存键类型</typeparam>
    /// <typeparam name="TValue">被缓存的对象类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns></returns>
    Task<TValue> GetObjectAsync<TKey, TValue>(TKey key);

    /// <summary>
    /// 设置对象到缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expire">过期时间</param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    Task SetObjectAsync<TKey, TValue>(TKey key, TValue value, DateTime expire);

    /// <summary>
    /// 根据键删除缓存
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    Task RemoveAsync<TKey>(TKey key);
}