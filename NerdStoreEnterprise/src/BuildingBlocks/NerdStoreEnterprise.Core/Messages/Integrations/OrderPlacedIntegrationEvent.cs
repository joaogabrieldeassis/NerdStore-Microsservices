namespace NerdStoreEnterprise.Core.Messages.Integrations;

public class OrderPlacedIntegrationEvent(Guid customerId) : IntegrationEvent
{
    public Guid CustomerId { get; private set; } = customerId;
}