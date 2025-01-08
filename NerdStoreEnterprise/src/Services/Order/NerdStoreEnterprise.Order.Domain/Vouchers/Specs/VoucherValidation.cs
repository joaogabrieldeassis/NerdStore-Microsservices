using NetDevPack.Specification;

namespace NerdStoreEnterprise.Order.Domain.Vouchers.Specs;

public class VoucherValidation : SpecValidator<Voucher>
{
    public VoucherValidation()
    {
        var dateSpec = new VoucherDateSpecification();
        var quantitySpec = new VoucherQuantitySpecification();
        var activeSpec = new ActiveVoucherSpecification();

        Add("dateSpec", new Rule<Voucher>(dateSpec, "This voucher is expired"));
        Add("quantitySpec", new Rule<Voucher>(quantitySpec, "This voucher has already been used"));
        Add("activeSpec", new Rule<Voucher>(activeSpec, "This voucher is no longer active"));
    }
}
