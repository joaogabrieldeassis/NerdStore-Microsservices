using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Catalog.Business.Models;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string Image { get; private set; } = string.Empty;
    public int StockQuantity { get; private set; }
}