using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Services;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Business.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task AddAsync(Product product)
    {
        await _repository.Add(product);
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}