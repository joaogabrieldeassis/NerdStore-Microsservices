using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Interfaces;
using Dapper.Contrib.Extensions;

namespace NerdStoreEnterprise.Catalog.Business.Models; 

[Table("NerdStore.dbo.Produtos")]
public class Product : Entity, IAggregateRoot
{
    private Product()
    {

    }

    public Product(string name, string description, bool isActive, decimal price, string image, int stockQuantity)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        Price = price;
        CreatedDate = DateTime.Now;
        Image = image;
        StockQuantity = stockQuantity;
    }
    
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } 
    public decimal Price { get; private set; } 
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public string Image { get; private set; } = string.Empty;
    public int StockQuantity { get; private set; }

    public void Update(string name, string description, bool isActive, decimal price, string image, int stockQuantity)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        Price = price;
        Image = image;
        StockQuantity = stockQuantity;
    }
}