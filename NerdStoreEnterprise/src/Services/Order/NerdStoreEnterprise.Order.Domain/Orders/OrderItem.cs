using NerdStoreEnterprise.Core.DomainObjects;

namespace NerdStoreEnterprise.Order.Domain.Orders;

public class OrderItem : Entity
{
    // EF ctor
    protected OrderItem() { }

    public OrderItem(Guid productId, string productName, int quantity,
        decimal unitPrice, string productImage = null)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ProductImage = productImage;
    }    

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string ProductImage { get; set; }

    // EF Rel.
    public Order Order { get; set; }    

    internal decimal CalculateValue()
    {
        return Quantity * UnitPrice;
    }
}
