using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.Api.Configurations.Caches;
using NerdStoreEnterprise.Catalog.Business.Interfaces.Repositories;
using NerdStoreEnterprise.Catalog.Infraestructure.Contexts;
using NerdStoreEnterprise.Catalog.Infraestructure.Factorys;
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
        services.AddScoped<DbContextFactory>();
    }

    public static void CacheConfig(this IServiceCollection services)
    {
        services.AddMemoryCache()
       .AddDistributedMemoryCache()
       .AddSingleton<ICacheManager, CacheManager>();
    }

    public static void MediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
    }
}