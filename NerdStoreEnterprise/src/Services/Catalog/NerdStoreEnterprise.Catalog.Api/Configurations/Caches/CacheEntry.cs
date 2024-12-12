namespace NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

[Serializable]
public sealed record CacheEntry<T>(T Data, TimeSpan? SlidingExpiration)
{
    public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
}