using MediatR;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.Api.Configurations.Caches;
using NerdStoreEnterprise.Catalog.Infraestructure.Contexts;

namespace NerdStoreEnterprise.Catalog.Api.Behaviors;

public class TransactionBehavior<TRequest, TResponse>(CatalogContext dbContext, ICacheManager cacheManager, IMediator mediator)
: IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly CatalogContext _dbContext = dbContext;
    private readonly ICacheManager _cacheManager = cacheManager;
    private readonly IMediator _mediator = mediator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_dbContext.Database.CurrentTransaction is not null)
        {
            return await next();
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
        try
        {
            var response = await next();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await _cacheManager.InvalidateAllAsync(cancellationToken);

            return response;
        }
        catch
        {
            // Automatic rollback is already provided by 'await using'
            throw; // Re-throw to maintain the original error behavior.
        }
    }
}
