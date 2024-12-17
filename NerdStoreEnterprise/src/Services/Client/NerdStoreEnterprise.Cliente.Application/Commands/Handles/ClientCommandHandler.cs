using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.Cliente.Domain.Models;
using NerdStoreEnterprise.Core.DomainObjects.ValueObjects;

namespace NerdStoreEnterprise.Cliente.Application.Commands.Handles;

public class ClientCommandHandler : IRequestHandler<RegisterClientCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult!;

        
        var client = new Client(request.Id, request.Name, new Email(request.Email), new Cpf(request.Cpf));
        return request.ValidationResult!;
    }
}