namespace NerdStoreEntripese.WebApp.MVC.Models.Responses;

public class UserToken
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public IEnumerable<UserClaim>? ClaimsDto { get; set; }
}
