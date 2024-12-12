using MediatR;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Responses.Handlers;

public sealed class ProductAllResponseHandler(IProductRepository repository) : IRequestHandler<ProductAllQuerie, IEnumerable<Product>>
{
    private readonly IProductRepository _repository = repository;

    public async Task<IEnumerable<Product>> Handle(ProductAllQuerie request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}