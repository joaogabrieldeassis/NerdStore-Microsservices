using NerdStoreEnterprise.Identity.Api.Models;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public interface IAutenticationService
{
    public Task<string> Login(Models.Users.LoginUser loginUser);
    public Task<string> Register(Models.Users.RegisterUser registerUser);
}