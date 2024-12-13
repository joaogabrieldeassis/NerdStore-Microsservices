namespace NerdStoreEnterprise.Cliente.Domain.Models.ValueObjects;

public class Address(string street, string number, string complement, string neighborhood, string zipCode, string city, string state)
{
    public string Street { get; private set; } = street;
    public string Number { get; private set; } = number;
    public string Complement { get; private set; } = complement;
    public string Neighborhood { get; private set; } = neighborhood;
    public string ZipCode { get; private set; } = zipCode;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public Guid? CustomerId { get; private set; }

    public Client? Client { get; protected set; }
}