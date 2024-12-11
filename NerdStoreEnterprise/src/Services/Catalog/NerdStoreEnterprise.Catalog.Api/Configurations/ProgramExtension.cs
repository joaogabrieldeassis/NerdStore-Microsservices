using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.Business.Interfaces;
using NerdStoreEnterprise.Catalog.Infraestructure.Contexts;
using NerdStoreEnterprise.Catalog.Infraestructure.Repository;

namespace NerdStoreEnterprise.Catalog.Api.Configurations;

public static class ProgramExtension
{
    public static void ConnectionDataBaseConfiguration(this IServiceCollection services, string connection)
    {
        services.AddDbContext<CatalogContext>(options =>
       options.UseSqlServer(connection));
    }

    public static void DependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<CatalogContext>();
    }
}