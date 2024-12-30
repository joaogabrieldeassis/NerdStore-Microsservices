using FluentValidation;

namespace NerdStoreEnterprise.Cart.Api.Models;

public class CartItemValidation : AbstractValidator<CartItem>
{
    public CartItemValidation()
    {
        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product ID");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Product name not provided");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage(item => $"The minimum quantity for {item.Name} is 1");

        RuleFor(c => c.Quantity)
            .LessThanOrEqualTo(CustomerCart.MAX_ITEM_QUANTITY)
            .WithMessage(item => $"The maximum quantity for {item.Name} is {CustomerCart.MAX_ITEM_QUANTITY}");

        RuleFor(c => c.Value)
            .GreaterThan(0)
            .WithMessage(item => $"The value of {item.Name} must be greater than 0");
    }
}