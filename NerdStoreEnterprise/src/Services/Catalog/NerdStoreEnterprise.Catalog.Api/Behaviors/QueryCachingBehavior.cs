using MediatR;
using NerdStoreEnterprise.Catalog.Api.Configurations.Caches;
using System.Text.Json;

namespace NerdStoreEnterprise.Catalog.Api.Behaviors;

public sealed class QueryCachingBehavior<TRequest, TResponse>(ICacheManager cacheManager) : IPipelineBehavior<TRequest, TResponse?>
    where TRequest : IQuery<TResponse>
{
    private readonly ICacheManager _cacheManager = cacheManager;

    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
    {
        var cacheKey = $"{typeof(TRequest).Name}-{JsonSerializer.Serialize(request)}";

        TResponse? response;

        try
        {
            response = await _cacheManager.GetAsync<TResponse?>(cacheKey, cancellationToken);
        }
        catch (Exception)
        {
            response = await next();
        }

        response ??= await next();

        await _cacheManager.SetAsync(cacheKey, response, TimeSpan.FromDays(1), TimeSpan.FromMinutes(30), cancellationToken);
        return response;
    }
}