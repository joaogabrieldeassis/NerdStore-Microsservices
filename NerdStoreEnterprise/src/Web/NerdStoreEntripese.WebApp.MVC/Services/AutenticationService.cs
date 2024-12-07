using System.Text;
using System.Text.Json;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public class AutenticationService(HttpClient httpClient) : IAutenticationService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<string> Login(Models.Users.LoginUser loginUser)
    {
        var loginContent = new StringContent(JsonSerializer.Serialize(loginUser),
                                             Encoding.UTF8,
                                             "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/logar", loginContent);

        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }

    public async Task<string> Register(Models.Users.RegisterUser registerUser)
    {
        try
        {
            var json = JsonSerializer.Serialize(registerUser);
            var loginContent = new StringContent(json,
                                                 Encoding.UTF8,
                                                 "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7225/Authentication/registrar", loginContent);

            var a = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
            return a;
        }
        catch (Exception e)
        {

            throw;
        }
        
        
    }
}