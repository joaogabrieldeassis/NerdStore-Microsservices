namespace NerdStoreEnterprise.Identity.Api.Models;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public double ExpiresIn { get; set; }

    public UserToken UserTokenDto { get; set; } = new();
}
