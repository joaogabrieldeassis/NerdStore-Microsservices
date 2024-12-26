namespace NerdStoreEnterprise.Core.Messages.Integrations;

public class UserRegisteredIntegrationEvent(Guid id, string name, string email, string cpf) : IntegrationEvent
{
    public Guid Id { get; set; } = id;
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string Cpf { get; } = cpf;
}