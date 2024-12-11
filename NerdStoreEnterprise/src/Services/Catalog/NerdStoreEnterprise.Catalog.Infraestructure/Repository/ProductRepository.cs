using NerdStoreEnterprise.Catalog.Business.Interfaces;
using NerdStoreEnterprise.Catalog.Business.Models;
using NerdStoreEnterprise.Catalog.Infraestructure.Contexts;
using NerdStoreEnterprise.Catalog.Infraestructure.Factorys;

namespace NerdStoreEnterprise.Catalog.Infraestructure.Repository;

public class ProductRepository(CatalogContext contextEntity, DbContextFactory connectionDapper) : IProductRepository
{
    private readonly CatalogContext _contextEntity = contextEntity;
    private readonly DbContextFactory _connectionDapper = connectionDapper;

    public void Add(Product produto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}