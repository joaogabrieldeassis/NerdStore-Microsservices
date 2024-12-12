using MediatR;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Services;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Requests.Handlers;

public class ProductUpdateHandler(IProductService service, IProductRepository repository) : IRequestHandler<ProductUpdate, bool>
{
    private readonly IProductService _service = service;
    private readonly IProductRepository _repository = repository;
    public async Task<bool> Handle(ProductUpdate request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if(product is null) return false;

        product.Update(request.Name,
                       request.Description,
                       request.IsActive,
                       request.Price,
                       request.Image,
                       request.StockQuantity);

        _service.Update(product);

        return true;
    }
}
