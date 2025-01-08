using NerdStoreEnterprise.Order.Domain.Orders;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.DTOs;

public class OrderItemDTO
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Quantity { get; set; }

    public static OrderItem ToOrderItem(OrderItemDTO orderItemDTO)
    {
        return new OrderItem(orderItemDTO.ProductId, orderItemDTO.Name, orderItemDTO.Quantity,
            orderItemDTO.Price, orderItemDTO.Image);
    }
}
