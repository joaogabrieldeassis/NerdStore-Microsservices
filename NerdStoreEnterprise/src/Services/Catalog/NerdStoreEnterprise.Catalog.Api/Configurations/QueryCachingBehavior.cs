using MediatR;
using System.Text.Json;

namespace NerdStoreEnterprise.Catalog.Api.Configurations;

public sealed class QueryCachingBehavior<TRequest, TResponse>(ICacheManager cacheManager) : IPipelineBehavior<TRequest, TResponse?>
    where TRequest : IRequest<TResponse>
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

        await _cacheManager.SetAsync(cacheKey, response, TimeSpan.FromHours(1), TimeSpan.FromMinutes(30), cancellationToken);
        return response;
    }
}