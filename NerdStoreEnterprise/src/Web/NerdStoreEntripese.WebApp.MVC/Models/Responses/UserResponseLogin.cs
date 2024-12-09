namespace NerdStoreEntripese.WebApp.MVC.Models.Responses;

public class UserResponseLogin
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserToken UsuarioToken { get; set; } = new();
    public ResponseResult ResponseResult { get; set; } = new ResponseResult();
}