using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Cliente.Application.Commands;
using NerdStoreEnterprise.Core.MediatR;
using NerdStoreEnterprise.Core.Messages.Integrations;
using NerdStoreEnterprise.MessageBus;

namespace NerdStoreEnterprise.Client.Infraestructure.Service;

public class RegisterClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus messageBus) : BackgroundService
{
    private IMessageBus _bus = messageBus;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private async Task<ResponseMessage> RegisterClient(UserRegisteredIntegrationEvent command)
    {
        var clientCommand = new RegisterClientCommand(command.Id, command.Name, command.Email, command.Cpf);
        ValidationResult result;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediatR = scope.ServiceProvider.GetRequiredService<IMediatRHandler>();
            result = await mediatR.SendCommand(clientCommand);
        }

        return new ResponseMessage(result);
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
            await RegisterClient(request));

        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }
}