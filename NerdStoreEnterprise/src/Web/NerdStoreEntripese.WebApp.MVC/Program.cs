using NerdStoreEntripese.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();
app.UseIdentityConfiguration();


app.UseMvcConfiguration(app.Environment);

app.Run();
