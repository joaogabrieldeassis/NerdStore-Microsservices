using FluentValidation.Results;
using NerdStoreEnterprise.Core.Messages;
using NerdStoreEnterprise.Order.Domain.Orders;
using NerdStoreEnterprise.Order.Domain.Vouchers;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Orders.Commands;

public class OrderCommandHandler : CommandHandler,
IRequestHandler<AddOrderCommand, ValidationResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;

    public OrderCommandHandler(IVoucherRepository voucherRepository,
                               IOrderRepository orderRepository)
    {
        _voucherRepository = voucherRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
    {
        // Command validation
        if (!message.IsValid()) return message.ValidationResult;

        // Map Order
        var order = MapOrder(message);

        // Apply voucher if any
        if (!await ApplyVoucher(message, order)) return ValidationResult;

        // Validate order
        if (!ValidateOrder(order)) return ValidationResult;

        // Process payment
        if (!ProcessPayment(order)) return ValidationResult;

        // If payment is all good!
        order.AuthorizeOrder();

        // Add Event
        order.AddEvent(new OrderPlacedEvent(order.Id, order.CustomerId));

        // Add Order to Repository
        _orderRepository.Add(order);

        // Persist order and voucher data
        return await PersistData(_orderRepository.UnitOfWork);
    }

    private Order MapOrder(AddOrderCommand message)
    {
        var address = new Address
        {
            Street = message.Address.Street,
            Number = message.Address.Number,
            Complement = message.Address.Complement,
            Neighborhood = message.Address.Neighborhood,
            ZipCode = message.Address.ZipCode,
            City = message.Address.City,
            State = message.Address.State
        };

        var order = new Order(message.CustomerId, message.TotalAmount, message.OrderItems.Select(OrderItemDTO.ToOrderItem).ToList(),
            message.VoucherUsed, message.Discount);

        order.AssignAddress(address);
        return order;
    }

    private async Task<bool> ApplyVoucher(AddOrderCommand message, Order order)
    {
        if (!message.VoucherUsed) return true;

        var voucher = await _voucherRepository.GetVoucherByCode(message.VoucherCode);
        if (voucher == null)
        {
            AddError("The provided voucher does not exist!");
            return false;
        }

        var voucherValidation = new VoucherValidation().Validate(voucher);
        if (!voucherValidation.IsValid)
        {
            voucherValidation.Errors.ToList().ForEach(m => AddError(m.ErrorMessage));
            return false;
        }

        order.AssignVoucher(voucher);
        voucher.DebitQuantity();

        _voucherRepository.Update(voucher);

        return true;
    }

    private bool ValidateOrder(Order order)
    {
        var originalOrderAmount = order.TotalAmount;
        var orderDiscount = order.Discount;

        order.CalculateOrderAmount();

        if (order.TotalAmount != originalOrderAmount)
        {
            AddError("The total order amount does not match the order calculation");
            return false;
        }

        if (order.Discount != orderDiscount)
        {
            AddError("The total amount does not match the order calculation");
            return false;
        }

        return true;
    }

    public bool ProcessPayment(Order order)
    {
        return true;
    }
}
