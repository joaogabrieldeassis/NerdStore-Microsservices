using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Interfaces;
using NerdStoreEnterprise.Order.Domain.Vouchers.Specs;

namespace NerdStoreEnterprise.Order.Domain.Vouchers;

public class Voucher : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType DiscountType { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UsageDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool Active { get; private set; }
    public bool Used { get; private set; }

    public bool IsValidForUse()
    {
        return new ActiveVoucherSpecification()
            .And(new VoucherDateSpecification())
            .And(new VoucherQuantitySpecification())
            .IsSatisfiedBy(this);
    }

    public void MarkAsUsed()
    {
        Active = false;
        Used = true;
        Quantity = 0;
        UsageDate = DateTime.Now;
    }

    public void DebitQuantity()
    {
        Quantity -= 1;
        if (Quantity >= 1) return;

        MarkAsUsed();
    }
}
