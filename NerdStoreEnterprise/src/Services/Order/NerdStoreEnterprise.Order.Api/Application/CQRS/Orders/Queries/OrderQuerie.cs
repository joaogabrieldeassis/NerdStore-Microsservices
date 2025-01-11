using Dapper;
using NerdStoreEnterprise.Order.Api.Application.CQRS.DTOs;
using NerdStoreEnterprise.Order.Domain.Orders;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Orders.Queries
{
    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid customerId);
        Task<IEnumerable<OrderDTO>> GetListByCustomerId(Guid customerId);
    }

    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> GetLastOrder(Guid customerId)
        {
            const string sql = @"SELECT
                            P.ID AS 'ProductId', P.CODE, P.VOUCHERUSED, P.DISCOUNT, P.TOTALVALUE, P.ORDERSTATUS,
                            P.STREET, P.NUMBER, P.NEIGHBORHOOD, P.ZIPCODE, P.COMPLEMENT, P.CITY, P.STATE,
                            PIT.ID AS 'ProductItemId', PIT.PRODUCTNAME, PIT.QUANTITY, PIT.PRODUCTIMAGE, PIT.UNITVALUE 
                            FROM ORDERS P 
                            INNER JOIN ORDERITEMS PIT ON P.ID = PIT.ORDERID 
                            WHERE P.CUSTOMERID = @customerId 
                            AND P.CREATIONDATE between DATEADD(minute, -3, GETDATE()) and DATEADD(minute, 0, GETDATE())
                            AND P.ORDERSTATUS = 1 
                            ORDER BY P.CREATIONDATE DESC";

            var order = await _orderRepository.GetConnection()
                .QueryAsync<dynamic>(sql, new { customerId });

            return MapOrder(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetListByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetListByCustomerId(customerId);

            return orders.Select(OrderDTO.ToOrderDTO);
        }

        private OrderDTO MapOrder(dynamic result)
        {
            var order = new OrderDTO
            {
                Code = result[0].CODE,
                Status = result[0].ORDERSTATUS,
                TotalAmount = result[0].TOTALVALUE,
                Discount = result[0].DISCOUNT,
                VoucherUsed = result[0].VOUCHERUSED,

                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO
                {
                    Street = result[0].STREET,
                    Neighborhood = result[0].NEIGHBORHOOD,
                    ZipCode = result[0].ZIPCODE,
                    City = result[0].CITY,
                    Complement = result[0].COMPLEMENT,
                    State = result[0].STATE,
                    Number = result[0].NUMBER
                }
            };

            foreach (var item in result)
            {
                var orderItem = new OrderItemDTO
                {
                    Name = item.PRODUCTNAME,
                    Value = item.UNITVALUE,
                    Quantity = item.QUANTITY,
                    Image = item.PRODUCTIMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }
    }

}
