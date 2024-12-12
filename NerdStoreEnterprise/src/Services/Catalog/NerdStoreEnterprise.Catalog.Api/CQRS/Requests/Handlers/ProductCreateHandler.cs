using MediatR;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Services;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Requests.Handlers;

public class ProductCreateHandler(IProductService service) : IRequestHandler<ProductCreate, Guid>
{
    private readonly IProductService _service = service;

    public async Task<Guid> Handle(ProductCreate request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name,
                                  request.Description,
                                  request.IsActive,
                                  request.Price,
                                  request.Image,
                                  request.StockQuantity);

        await _service.AddAsync(product);

        return product.Id;
    }
}
