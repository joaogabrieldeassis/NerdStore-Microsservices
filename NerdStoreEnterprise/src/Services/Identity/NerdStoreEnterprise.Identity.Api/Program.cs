using NerdStoreEnterprise.Services.Identity;
using NerdStoreEnterprise.Identity.ConfigurationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Identity
builder.Services.ConfigureIdentityService(builder.Configuration.GetConnectionString("DefaultConnectionSql")!);

var secret = builder.Configuration.GetSection("AppSettings:Secret").Value!;
var validIn = builder.Configuration.GetSection("AppSettings:ValidIn").Value!;
var issuer = builder.Configuration.GetSection("AppSettings:Emissor").Value!;
builder.Services.RegisterJwt(secret, validIn, issuer);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
