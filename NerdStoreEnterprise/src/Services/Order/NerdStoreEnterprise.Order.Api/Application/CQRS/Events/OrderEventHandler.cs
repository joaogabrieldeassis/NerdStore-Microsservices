﻿using MediatR;
using NerdStoreEnterprise.Core.Messages.Integrations;
using NerdStoreEnterprise.MessageBus;

namespace NerdStoreEnterprise.Order.Api.Application.CQRS.Events;

public class OrderEventHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly IMessageBus _bus;

    public OrderEventHandler(IMessageBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderPlacedEvent message, CancellationToken cancellationToken)
    {
        await _bus.PublishAsync(new OrderPlacedIntegrationEvent(message.ClientId));
    }
}
{

}