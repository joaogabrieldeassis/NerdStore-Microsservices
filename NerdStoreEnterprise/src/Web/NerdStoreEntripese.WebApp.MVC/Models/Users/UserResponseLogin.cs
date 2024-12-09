namespace NerdStoreEntripese.WebApp.MVC.Models.Users;

public class UserResponseLogin
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserToken? UsuarioToken { get; set; }
}