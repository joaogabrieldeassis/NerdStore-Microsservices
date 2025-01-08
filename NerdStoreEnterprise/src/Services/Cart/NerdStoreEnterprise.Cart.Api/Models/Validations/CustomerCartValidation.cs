using FluentValidation;

namespace NerdStoreEnterprise.Cart.Api.Models;

public class CustomerCartValidation : AbstractValidator<ClientCart>
{
    public CustomerCartValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Unrecognized customer");

        RuleFor(c => c.Items.Count)
            .GreaterThan(0)
            .WithMessage("The cart has no items");

        RuleFor(c => c.TotalValue)
            .GreaterThan(0)
            .WithMessage("The total cart value must be greater than 0");
    }
}