using MediatR;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Responses.Handlers;

public sealed class ProductByIdResponseHandler(IProductRepository repository) : IRequestHandler<ProductByIdQuerie, Product>
{
    private readonly IProductRepository _repository = repository;

    public async Task<Product> Handle(ProductByIdQuerie request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}