using NerdStoreEnterprise.Catalog.Api.Configurations;
using NerdStoreEnterprise.Services.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConnectionDataBaseConfiguration(builder.Configuration.GetConnectionString("DefaultConnectionSql")!);
builder.Services.DependencyInjectionConfig();
builder.Services.MediatRConfig();
builder.Services.CacheConfig();

var secret = builder.Configuration.GetSection("AppSettings:Secret").Value!;
var validIn = builder.Configuration.GetSection("AppSettings:ValidIn").Value!;
var issuer = builder.Configuration.GetSection("AppSettings:Emissor").Value!;
builder.Services.RegisterJwt(secret, validIn, issuer);

builder.Services.RegisterSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();