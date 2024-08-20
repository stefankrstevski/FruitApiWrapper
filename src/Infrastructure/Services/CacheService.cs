using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class CacheService<T> : ICacheService<T> where T : class
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24); // after 24 cache expires

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T> GetCachedAsync(string key)
    {
        _cache.TryGetValue(key, out T cachedItem);
        return Task.FromResult(cachedItem);
    }

    public Task SetCachedAsync(string key, T item)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(_cacheDuration);

        _cache.Set(key, item, cacheEntryOptions);
        return Task.CompletedTask;
    }
}
