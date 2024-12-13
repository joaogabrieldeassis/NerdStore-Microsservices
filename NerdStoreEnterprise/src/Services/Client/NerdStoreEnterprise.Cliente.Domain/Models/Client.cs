using NerdStoreEnterprise.Cliente.Domain.Models.ValueObjects;
using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.DomainObjects.ValueObjects;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Cliente.Domain.Models;

public class Client : Entity, IAggregateRoot
{
    public Client(Guid id, string name, Email email, Cpf cpf, bool deleted, Address address)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
        Deleted = deleted;
        Address = address;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; } 
    public bool Deleted { get; private set; } = false;
    public Address Address { get; private set; }


    public void ChangeEmail(string email)
    {
        Email = new Email(email);
    }

    public void AssignAddress(Address address)
    {
        Address = address;
    }
}