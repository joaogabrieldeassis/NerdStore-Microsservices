using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Services;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Business.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<Guid> AddAsync(Product product)
    {
        await _repository.Add(product);
    }

    public Task Update(Product product)
    {
        throw new NotImplementedException();
    }
}