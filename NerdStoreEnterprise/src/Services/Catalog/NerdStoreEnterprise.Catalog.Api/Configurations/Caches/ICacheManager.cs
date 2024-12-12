namespace NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

public interface ICacheManager
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);
    Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null, CancellationToken cancellationToken = default);
    Task InvalidateAsync(string key, CancellationToken cancellationToken);
    Task InvalidateAllAsync(CancellationToken cancellationToken, string? baseCacheKey = null);
}