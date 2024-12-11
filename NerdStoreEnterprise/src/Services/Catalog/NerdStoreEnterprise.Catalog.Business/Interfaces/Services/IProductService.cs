using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Business.Interfaces.Services;

public interface IProductService
{
    public Task<Guid> AddAsync(Product product);
    public Task Update(Product product);
}