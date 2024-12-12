using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

public sealed class CacheManager(IDistributedCache cache) : ICacheManager
{
    private readonly IDistributedCache _cache = cache;
    private static readonly object Lock = new();
    private static readonly HashSet<string> CacheKeys = [];

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        var cacheData = await _cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(cacheData))
            return default;

        var cacheEntry = JsonSerializer.Deserialize<CacheEntry<T>>(cacheData);
        if (cacheEntry == null)
            return default;

        if (cacheEntry.SlidingExpiration.HasValue)
        {
            cacheEntry.LastAccessed = DateTime.UtcNow;
            await SetAsync(key, cacheEntry.Data, TimeSpan.FromHours(1), cacheEntry.SlidingExpiration, cancellationToken);
        }

        return cacheEntry.Data;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null, CancellationToken cancellationToken = default)
    {
        var cacheEntry = new CacheEntry<T>(value, slidingExpireTime);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromHours(1), // Default 1 hour
            SlidingExpiration = slidingExpireTime
        };

        var serializedData = JsonSerializer.Serialize(cacheEntry);
        await _cache.SetStringAsync(key, serializedData, options, cancellationToken);

        lock (Lock)
        {
            CacheKeys.Add(key);
        }
    }

    public async Task InvalidateAsync(string key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(key, cancellationToken);
        lock (Lock)
        {
            CacheKeys.Remove(key);
        }
    }

    public async Task InvalidateAllAsync(CancellationToken cancellationToken, string? baseCacheKey = null)
    {
        var keysToInvalidate = new List<string>();

        lock (Lock)
        {
            if (string.IsNullOrEmpty(baseCacheKey))
            {
                keysToInvalidate.AddRange(CacheKeys);
            }
            else
            {
                foreach (var key in CacheKeys)
                {
                    if (key.Contains(baseCacheKey))
                    {
                        keysToInvalidate.Add(key);
                    }
                }
            }

            foreach (var key in keysToInvalidate)
            {
                CacheKeys.Remove(key);
            }
        }

        foreach (var key in keysToInvalidate)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}