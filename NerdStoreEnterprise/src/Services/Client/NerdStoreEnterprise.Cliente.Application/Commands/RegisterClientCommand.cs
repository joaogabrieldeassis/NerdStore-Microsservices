using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Cliente.Application.Commands;

public class RegisterClientCommand : Command
{
    public RegisterClientCommand(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}