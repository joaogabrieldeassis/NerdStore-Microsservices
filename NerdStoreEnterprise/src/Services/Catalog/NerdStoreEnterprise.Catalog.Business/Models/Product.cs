using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Catalog.Business.Models;

public class Product(string name, string description, bool isActive, decimal price, string image, int stockQuantity) : Entity, IAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public bool IsActive { get; private set; } = isActive;
    public decimal Price { get; private set; } = price;
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public string Image { get; private set; } = image;
    public int StockQuantity { get; private set; } = stockQuantity;

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