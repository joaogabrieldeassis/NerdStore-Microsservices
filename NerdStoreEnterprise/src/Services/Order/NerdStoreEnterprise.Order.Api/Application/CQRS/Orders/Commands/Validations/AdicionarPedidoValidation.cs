using FluentValidation;
using NerdStoreEnterprise.Order.Api.Application.CQRS.Orders.Commands;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Vouchers.Orders.Validations;

public class AddOrderValidation : AbstractValidator<AddOrderCommand>
{
    public AddOrderValidation()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid customer ID");

        RuleFor(c => c.OrderItems.Count)
            .GreaterThan(0)
            .WithMessage("The order must have at least 1 item");

        RuleFor(c => c.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Invalid order amount");

        RuleFor(c => c.CardNumber)
            .CreditCard()
            .WithMessage("Invalid card number");

        RuleFor(c => c.CardName)
            .NotNull()
            .WithMessage("Cardholder name is required");

        RuleFor(c => c.CardCvv.Length)
            .GreaterThan(2)
            .LessThan(5)
            .WithMessage("The card CVV must be 3 or 4 digits");

        RuleFor(c => c.CardExpiration)
            .NotNull()
            .WithMessage("Card expiration date is required");
    }
}