namespace NerdStoreEntripese.WebApp.MVC.Models.Users;

public class UserToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<UserClaim> Claims { get; set; } = [];
}
