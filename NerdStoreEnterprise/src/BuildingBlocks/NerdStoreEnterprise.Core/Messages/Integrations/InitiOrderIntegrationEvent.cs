namespace NerdStoreEnterprise.Core.Messages.Integrations;

public class OrderStartedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public int PaymentType { get; set; }
    public decimal Amount { get; set; }

    public string CardName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string ExpirationMonthYear { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
}