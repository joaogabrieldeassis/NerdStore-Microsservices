using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Cliente.Application.Commands;
using NerdStoreEnterprise.Cliente.Application.Commands.Handles;
using NerdStoreEnterprise.Core.MediatR;

namespace NerdStoreEnterprise.Cliente.Infraestructure.CrossCutting.Ioc;

public static class DependencyInjection
{

    public static void RegisterServices(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IMediatRHandler, MediatRHandler>();
        serviceDescriptors.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
    }
}