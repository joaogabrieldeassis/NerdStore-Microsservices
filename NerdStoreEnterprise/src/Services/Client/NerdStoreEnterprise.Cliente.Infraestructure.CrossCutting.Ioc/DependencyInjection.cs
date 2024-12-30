using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Cliente.Application.Commands;
using NerdStoreEnterprise.Cliente.Application.Commands.Events;
using NerdStoreEnterprise.Cliente.Application.Commands.Handles;
using NerdStoreEnterprise.Cliente.Domain.Models.Interfaces.Repositories;
using NerdStoreEnterprise.Cliente.Infraestructure.Data.Context;
using NerdStoreEnterprise.Cliente.Infraestructure.Data.Repositories;
using NerdStoreEnterprise.Core.MediatR;
using NerdStoreEnterprise.MessageBus;
using NerdStoreEnterprise.Core.Utils;
using NerdStoreEnterprise.Client.Infraestructure.Service;

namespace NerdStoreEnterprise.Cliente.Infraestructure.CrossCutting.Ioc;

public static class DependencyInjection
{

    public static void RegisterServices(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IMediatRHandler, MediatRHandler>();
        serviceDescriptors.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();

        serviceDescriptors.AddScoped<INotificationHandler<RegisteredClientEvent>, ClientEventHandler>();

        serviceDescriptors.AddScoped<IClientRepository, ClientRepository>();

        serviceDescriptors.AddScoped<ClientContext>();
    }

    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")!)
            .AddHostedService<RegisterClientIntegrationHandler>();
    }
}