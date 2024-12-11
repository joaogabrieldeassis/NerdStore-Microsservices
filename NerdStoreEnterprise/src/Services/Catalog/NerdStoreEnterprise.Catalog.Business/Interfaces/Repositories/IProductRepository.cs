using NerdStoreEnterprise.Catalog.Business.Models;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task Add(Product product);
    void Update(Product product);
}