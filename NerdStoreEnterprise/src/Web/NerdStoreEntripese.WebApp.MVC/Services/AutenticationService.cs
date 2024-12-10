using NerdStoreEntripese.WebApp.MVC.Models.Responses;
using System.Text;
using System.Text.Json;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public class AutenticationService(HttpClient httpClient, IConfiguration configuration) : Service, IAutenticationService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _urlApiIdentity = configuration.GetSection("Autentication").Value!;

    public async Task<ResponseRequest?> Login(Models.Users.LoginUser loginUser)
    {
        var optionsContext = GetContent(loginUser);

        var response = await _httpClient.PostAsync($"{_urlApiIdentity}/logar", optionsContext);

        if (TreatErrosResponse(response))
            return await DeserializarObjetoResponse<ResponseRequest>(response);

        return await DeserializarObjetoResponse<ResponseRequest>(response);
    }

    public async Task<ResponseRequest?> Register(Models.Users.RegisterUser registerUser)
    {
        var optionsContext = GetContent(registerUser);

        var response = await _httpClient.PostAsync($"{_urlApiIdentity}/registrar", optionsContext);

        if (TreatErrosResponse(response))
            return new ResponseRequest
            {
                Data = new UserResponseLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                }
            };

        return await DeserializarObjetoResponse<ResponseRequest>(response);
    }
}