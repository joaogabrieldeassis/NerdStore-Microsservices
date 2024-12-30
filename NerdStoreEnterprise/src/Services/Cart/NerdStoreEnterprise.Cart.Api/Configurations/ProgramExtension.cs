using NerdStoreEnterprise.Cart.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace NerdStoreEnterprise.Cart.Api.Configurations;

public static class ProgramExtension
{
    public static void ConnectionDataBaseConfiguration(this IServiceCollection services, string connection)
    {
        services.AddDbContext<CartContext>(options =>
       options.UseSqlServer(connection));
    }
}