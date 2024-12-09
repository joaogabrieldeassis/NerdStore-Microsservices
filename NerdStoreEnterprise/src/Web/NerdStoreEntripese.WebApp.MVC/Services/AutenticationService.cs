using NerdStoreEntripese.WebApp.MVC.Models.Responses;
using System.Text;
using System.Text.Json;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public class AutenticationService(HttpClient httpClient) : Service, IAutenticationService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ResponseRequest?> Login(Models.Users.LoginUser loginUser)
    {
        var loginContent = new StringContent(JsonSerializer.Serialize(loginUser),
                                             Encoding.UTF8,
                                             "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/logar", loginContent);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var options2 = await response.Content.ReadAsStringAsync();
        if (TreatErrosResponse(response))
            return JsonSerializer.Deserialize<ResponseRequest>(options2, options);

        return JsonSerializer.Deserialize<ResponseRequest>(options2, options);
    }

    public async Task<ResponseRequest?> Register(Models.Users.RegisterUser registerUser)
    {
        var json = JsonSerializer.Serialize(registerUser);
        var loginContent = new StringContent(json,
                                             Encoding.UTF8,
                                             "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/registrar", loginContent);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        if (TreatErrosResponse(response))
            return new ResponseRequest
            {
                Data = new UserResponseLogin
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                }                
            };

        return JsonSerializer.Deserialize<ResponseRequest>(await response.Content.ReadAsStringAsync(), options);
    }
}