using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NerdStoreEnterprise.Identity.Api.Data;
using System.Text;

namespace NerdStoreEnterprise.Identity.ConfigurationService;

public static class ConfigIdentity
{
    public static void ConfigureIdentityService(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secret = configuration.GetSection("AppSettings:Secret").Value;
        var validIn = configuration.GetSection("AppSettings:ValidIn").Value;
        var issuer = configuration.GetSection("AppSettings:Emissor").Value;

        var key = Encoding.ASCII.GetBytes(secret!);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = validIn,
                ValidIssuer = issuer
            };
        });
    }
}