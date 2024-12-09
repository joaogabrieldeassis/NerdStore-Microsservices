namespace NerdStoreEntripese.WebApp.MVC.Models.Responses;

public class UserResponseLogin
{
    public string? AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken? UserTokenDto { get; set; }
    public ResponseResult? ResponseResult { get; set; }
}