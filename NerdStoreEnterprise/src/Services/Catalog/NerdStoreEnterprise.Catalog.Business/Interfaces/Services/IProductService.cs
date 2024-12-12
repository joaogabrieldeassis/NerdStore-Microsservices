using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Business.Interfaces.Services;

public interface IProductService
{
    public Task AddAsync(Product product);
    public void Update(Product product);
}