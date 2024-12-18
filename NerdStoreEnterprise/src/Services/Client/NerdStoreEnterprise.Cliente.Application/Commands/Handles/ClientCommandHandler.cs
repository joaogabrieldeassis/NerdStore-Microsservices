using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.Cliente.Domain.Models;
using NerdStoreEnterprise.Cliente.Domain.Models.Interfaces.Repositories;
using NerdStoreEnterprise.Core.DomainObjects.ValueObjects;
using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Cliente.Application.Commands.Handles;

public class ClientCommandHandler(IClientRepository clientRepository) : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ValidationResult> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult!;
        
        var client = new Client(request.Id, request.Name, new Email(request.Email), new Cpf(request.Cpf));

        var clienteExistente = await _clientRepository.GetByCpf(client.Cpf.Numero);

        if (clienteExistente is null)
        {
            AddErros("Este CPF já está em uso.");
            return ValidationResult;
        }

        _clientRepository.Add(client);

        return await PersistData(_clientRepository.IUnitOfwork);
    }
}