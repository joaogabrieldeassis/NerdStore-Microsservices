using NerdStoreEnterprise.Core.Messages.Integrations;
using NerdStoreEnterprise.MessageBus;
using NerdStoreEnterprise.Payment.Api.Models;

namespace NerdStoreEnterprise.Payment.Api.Services;

public class PaymentIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IMessageBus _bus = bus;
    private readonly IServiceProvider _serviceProvider = serviceProvider;



    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    public void SetResponder()
    {
        _bus.RespondAsync<OrderStartedIntegrationEvent, ResponseMessage>(AutorizadPayment);
    }

    public async Task<ResponseMessage> AutorizadPayment(OrderStartedIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

        var payment = new Models.Payment
        {
            OrderId = message.OrderId,
            PaymentType = (PaymentType)message.PaymentType,
            Amount = message.Amount,
            CreditCard = new CreditCard(message.CardName,
                                        message.CardNumber,
                                        message.ExpirationMonthYear,
                                        message.CVV)     
        };

        return await paymentService.AutorizadPayment(payment);
    }
}
