using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Events;

public class OrderPlacedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }

    public OrderPlacedEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        ClientId = customerId;
    }
}
