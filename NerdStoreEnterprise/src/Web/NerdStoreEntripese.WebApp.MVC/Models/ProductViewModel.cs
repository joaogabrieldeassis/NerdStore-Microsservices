namespace NerdStoreEntripese.WebApp.MVC.Models;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Active { get; set; }
    public decimal Price { get; set; }
    public DateTime DateAdded { get; set; }
    public string Image { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}