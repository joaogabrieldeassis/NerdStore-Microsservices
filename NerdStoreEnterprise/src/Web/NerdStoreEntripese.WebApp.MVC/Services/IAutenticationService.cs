using NerdStoreEnterprise.Identity.Api.Models;
using NerdStoreEntripese.WebApp.MVC.Models.Responses;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public interface IAutenticationService
{
    public Task<ResponseRequest?> Login(Models.Users.LoginUser loginUser);
    public Task<ResponseRequest?> Register(Models.Users.RegisterUser registerUser);
}