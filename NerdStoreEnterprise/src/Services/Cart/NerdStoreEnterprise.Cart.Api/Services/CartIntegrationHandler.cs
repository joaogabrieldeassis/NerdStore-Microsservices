
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Cart.Api.Data;
using NerdStoreEnterprise.Core.Messages.Integrations;
using NerdStoreEnterprise.MessageBus;

namespace NerdStoreEnterprise.Cart.Api.Services;

public class CartIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IMessageBus _bus = bus;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetSubscribers();
        return Task.CompletedTask;
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<OrderPlacedIntegrationEvent>("Pedido realizado", DeletedCart);
    }

    private async Task DeletedCart(OrderPlacedIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CartContext>();

        var cart = await context.ClientCart.FirstOrDefaultAsync(x => x.ClientId == message.CustomerId);
        if(cart is not null)
        {
            context.ClientCart.Remove(cart);
            await context.SaveChangesAsync();
        }
    }

    


}