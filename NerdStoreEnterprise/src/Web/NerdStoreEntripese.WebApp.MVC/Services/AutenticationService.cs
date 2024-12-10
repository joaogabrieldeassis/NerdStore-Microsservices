using NerdStoreEntripese.WebApp.MVC.Models.Responses;
using System.Text;
using System.Text.Json;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public class AutenticationService(HttpClient httpClient) : Service, IAutenticationService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ResponseRequest?> Login(Models.Users.LoginUser loginUser)
    {
        var optionsContext = GetContent(loginUser);

        var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/logar", optionsContext);

        if (TreatErrosResponse(response))
            return await DeserializarObjetoResponse<ResponseRequest>(response);

        return await DeserializarObjetoResponse<ResponseRequest>(response);
    }

    public async Task<ResponseRequest?> Register(Models.Users.RegisterUser registerUser)
    {
        var optionsContext = GetContent(registerUser);

        var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/registrar", optionsContext);

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