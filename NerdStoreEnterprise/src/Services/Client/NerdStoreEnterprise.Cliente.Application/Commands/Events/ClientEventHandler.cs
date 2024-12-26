using MediatR;

namespace NerdStoreEnterprise.Cliente.Application.Commands.Events;

public class ClientEventHandler : INotificationHandler<RegisteredClientEvent>
{
    public Task Handle(RegisteredClientEvent notification, CancellationToken cancellationToken)
    {
        // Enviar evento de confirmação
        return Task.CompletedTask;
    }
}
