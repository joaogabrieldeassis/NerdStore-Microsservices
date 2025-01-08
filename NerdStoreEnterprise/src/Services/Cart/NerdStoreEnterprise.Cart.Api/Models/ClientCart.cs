
using FluentValidation.Results;

namespace NerdStoreEnterprise.Cart.Api.Models;

public partial class ClientCart
{
    internal const int MAX_ITEM_QUANTITY = 5;

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal TotalValue { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public ValidationResult ValidationResult { get; set; }

    public ClientCart(Guid customerId)
    {
        Id = Guid.NewGuid();
        ClientId = customerId;
    }

    public ClientCart() { }

    internal void CalculateCartValue()
    {
        TotalValue = Items.Sum(p => p.CalculateValue());
    }

    internal bool ExistingCartItem(CartItem item)
    {
        return Items.Any(p => p.ProductId == item.ProductId);
    }

    internal CartItem GetByProductId(Guid productId)
    {
        return Items.FirstOrDefault(p => p.ProductId == productId);
    }

    internal void AddItem(CartItem item)
    {
        item.AssociateCart(Id);

        if (ExistingCartItem(item))
        {
            var existingItem = GetByProductId(item.ProductId);
            existingItem.AddUnits(item.Quantity);

            item = existingItem;
            Items.Remove(existingItem);
        }

        Items.Add(item);
        CalculateCartValue();
    }

    internal void UpdateItem(CartItem item)
    {
        item.AssociateCart(Id);

        var existingItem = GetByProductId(item.ProductId);

        Items.Remove(existingItem);
        Items.Add(item);

        CalculateCartValue();
    }

    internal void UpdateUnits(CartItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    internal void RemoveItem(CartItem item)
    {
        Items.Remove(GetByProductId(item.ProductId));
        CalculateCartValue();
    }

    internal bool IsValid()
    {
        var errors = Items.SelectMany(i => new CartItemValidation().Validate(i).Errors).ToList();
        errors.AddRange(new CustomerCartValidation().Validate(this).Errors);
        ValidationResult = new ValidationResult(errors);

        return ValidationResult.IsValid;
    }
}