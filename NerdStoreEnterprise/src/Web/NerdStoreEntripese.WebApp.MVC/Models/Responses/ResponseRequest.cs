namespace NerdStoreEntripese.WebApp.MVC.Models.Responses;

public class ResponseRequest
{
    public bool Success { get; set; }
    public UserResponseLogin? Data { get; set; }
    public List<string> Errors { get; set; } = [];
}