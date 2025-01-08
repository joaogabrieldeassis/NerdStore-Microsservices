using NerdStoreEnterprise.Core.Interfaces;
using System.Data.Common;

namespace NerdStoreEnterprise.Order.Domain.Orders;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetById(Guid id);
    Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId);
    void Add(Order order);
    void Update(Order order);

    DbConnection GetConnection();


    /* Order Item */
    Task<OrderItem> GetItemById(Guid id);
    Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
}
