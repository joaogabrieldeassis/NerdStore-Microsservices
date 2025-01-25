namespace NerdStoreEnterprise.Core.Messages.Integrations;

public class OrderPlacedIntegrationEvent(Guid customerId, Guid orderId) : IntegrationEvent
{
    public Guid CustomerId { get; private set; } = customerId;
    public Guid OrderId { get; private set; } = orderId;    
}