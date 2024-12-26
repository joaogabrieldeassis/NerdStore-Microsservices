using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Cliente.Application.Commands;
using NerdStoreEnterprise.Core.MediatR;
using NerdStoreEnterprise.Core.Messages.Integrations;

namespace NerdStoreEnterprise.Client.Infraestructure.Service;

public class RegisterClientIntegrationHandler(IServiceProvider serviceProvider) : BackgroundService
{
    private IBus _bus = RabbitHutch.CreateBus("host=localhost:5672");
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request => new ResponseMessage(await RegisterClient(request)));

        return Task.CompletedTask;
    }

    private async Task<ValidationResult> RegisterClient(UserRegisteredIntegrationEvent command)
    {
        var clientCommand = new RegisterClientCommand(command.Id, command.Name, command.Email, command.Cpf);
        ValidationResult result;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediatR = scope.ServiceProvider.GetRequiredService<IMediatRHandler>();
            result = await mediatR.SendCommand(clientCommand);
        }

        return result;
    }
}