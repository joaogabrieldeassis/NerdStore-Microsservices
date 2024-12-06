namespace NerdStoreEnterprise.Identity.Api.Models;

public class UserToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Claim> ClaimsDto { get; set; } = [];
}