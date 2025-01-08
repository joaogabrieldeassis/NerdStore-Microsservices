using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Orders.Commands;

public class AddOrderCommand : Command
{
    // Order
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }

    // Voucher
    public string VoucherCode { get; set; }
    public bool VoucherUsed { get; set; }
    public decimal Discount { get; set; }

    // Address
    public AddressDTO Address { get; set; }

    // Card
    public string CardNumber { get; set; }
    public string CardName { get; set; }
    public string CardExpiration { get; set; }
    public string CardCvv { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}