using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Order.Domain.Vouchers;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> GetVoucherByCode(string code);
    void Update(Voucher voucher);
}