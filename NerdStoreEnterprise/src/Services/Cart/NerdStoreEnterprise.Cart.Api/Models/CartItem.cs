using System.Text.Json.Serialization;

namespace NerdStoreEnterprise.Cart.Api.Models;

public partial class CartItem
{
    public CartItem()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; }

    public Guid CartId { get; set; }

    [JsonIgnore]
    public CustomerCart CustomerCart { get; set; }

    internal void AssociateCart(Guid cartId)
    {
        CartId = cartId;
    }

    internal decimal CalculateValue()
    {
        return Quantity * Value;
    }

    internal void AddUnits(int units)
    {
        Quantity += units;
    }

    internal void UpdateUnits(int units)
    {
        Quantity = units;
    }

    internal bool IsValid()
    {
        return new CartItemValidation().Validate(this).IsValid;
    }
}