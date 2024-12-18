using NerdStoreEnterprise.Catalog.Business.Models;
using NerdStoreEnterprise.Catalog.Infraestructure.Contexts;
using NerdStoreEnterprise.Catalog.Infraestructure.Factorys;
using Microsoft.Data.SqlClient;
using Dapper;
using NerdStoreEnterprise.Catalog.Infraestructure.Queries;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Catalog.Infraestructure.Repository;

public class ProductRepository(CatalogContext contextEntity, DbContextFactory connectionDapper) : IProductRepository
{
    private readonly CatalogContext _contextEntity = contextEntity;
    private readonly SqlConnection _connectionDapper = connectionDapper.CreateConnection();

    public IUnitOfwork IUnitOfwork => _contextEntity;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _connectionDapper.QueryAsync<Product>(ProductQuerie.GetAll());
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _connectionDapper.QueryFirstAsync<Product>(ProductQuerie.GetAll(), new { Id = id });
    }

    public async Task Add(Product product)
    {
        await _contextEntity.AddAsync(product);
    }

    public void Update(Product product)
    {
        _contextEntity.Update(product);
    }
}