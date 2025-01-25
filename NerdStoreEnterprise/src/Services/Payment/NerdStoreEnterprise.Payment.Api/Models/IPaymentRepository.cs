using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Payment.Api.Models;

public interface IPaymentRepository : IRepository<Payment>
{
    void AddPayment(Payment payment);
    void AddTransaction(Transaction transaction);
    Task<Payment> GetPaymentByOrderId(Guid orderId);
    Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId);
}
