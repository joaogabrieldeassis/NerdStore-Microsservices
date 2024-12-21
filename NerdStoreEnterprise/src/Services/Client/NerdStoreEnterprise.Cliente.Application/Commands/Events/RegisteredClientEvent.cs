using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Cliente.Application.Commands.Events;

public class RegisteredClientEvent : Event
{
    public RegisteredClientEvent(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}
