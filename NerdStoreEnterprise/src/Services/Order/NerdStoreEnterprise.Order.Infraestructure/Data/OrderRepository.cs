using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Order.Domain.Orders;
using NetDevPack.Data;
using ;
using NerdStoreEnterprise.Core.Interfaces;
namespace NerdStoreEnterprise.Order.Infraestructure.Data
{
    public class OrderRepository(OrderContext context) : IOrderRepository
    {
        private readonly OrderContext _context = context;

        public IUnitOfwork IUnitOfwork => _context;

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public async Task<Domain.Orders.Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Orders.Order>> GetListByCustomerId(Guid customerId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public void Add(Domain.Orders.Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Domain.Orders.Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.ProductId == productId && oi.OrderId == orderId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}