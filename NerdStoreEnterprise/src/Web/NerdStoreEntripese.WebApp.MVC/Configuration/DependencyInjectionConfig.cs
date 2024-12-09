using NerdStoreEntripese.WebApp.MVC.Extensions;
using NerdStoreEntripese.WebApp.MVC.Extensions.Interfaces;
using NerdStoreEntripese.WebApp.MVC.Services;

namespace NerdStoreEntripese.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAutenticationService, AutenticationService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }
}