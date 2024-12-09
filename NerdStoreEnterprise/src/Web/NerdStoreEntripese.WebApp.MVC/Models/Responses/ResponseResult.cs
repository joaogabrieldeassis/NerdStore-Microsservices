namespace NerdStoreEntripese.WebApp.MVC.Models.Responses;

public class ResponseResult
{
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; } = new ResponseErrorMessages();
}