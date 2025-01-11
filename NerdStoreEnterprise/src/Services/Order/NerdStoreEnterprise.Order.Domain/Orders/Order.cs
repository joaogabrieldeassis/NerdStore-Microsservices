using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Interfaces;
using NerdStoreEnterprise.Order.Domain.Vouchers;

namespace NerdStoreEnterprise.Order.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    public Order(Guid customerId, decimal totalAmount, List<OrderItem> orderItems,
        bool voucherUsed = false, decimal discount = 0, Guid? voucherId = null)
    {
        CustomerId = customerId;
        TotalAmount = totalAmount;
        _orderItems = orderItems;

        Discount = discount;
        VoucherUsed = voucherUsed;
        VoucherId = voucherId;
    }

    // EF ctor
    protected Order() { }

    public int Code { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Address Address { get; private set; }

    // EF Rel.
    public Vouchers.Voucher Voucher { get; private set; }

    public void AuthorizeOrder()
    {
        OrderStatus = OrderStatus.Authorized;
    }

    public void AssignVoucher(Vouchers.Voucher voucher)
    {
        VoucherUsed = true;
        VoucherId = voucher.Id;
        Voucher = voucher;
    }

    public void AssignAddress(Address address)
    {
        Address = address;
    }

    public void CalculateOrderValue()
    {
        TotalAmount = OrderItems.Sum(p => p.CalculateValue());
        CalculateTotalDiscountValue();
    }

    public void CalculateTotalDiscountValue()
    {
        if (!VoucherUsed) return;

        decimal discount = 0;
        var value = TotalAmount;

        if (Voucher.DiscountType == VoucherDiscountType.Percentage)
        {
            if (Voucher.Percentage.HasValue)
            {
                discount = (value * Voucher.Percentage.Value) / 100;
                value -= discount;
            }
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                value -= discount;
            }
        }

        TotalAmount = value < 0 ? 0 : value;
        Discount = discount;
    }
}
