using NerdStoreEnterprise.Core.DomainObjects.Exeptions;
using System.Text.RegularExpressions;

namespace NerdStoreEnterprise.Core.DomainObjects.ValueObjects;

public class Email
{
    public const int AddressMaxLength = 254;
    public const int AddressMinLength = 5;
    public string Address { get; private set; } = string.Empty;

    // EntityFramework Constructor
    protected Email() { }

    public Email(string address)
    {
        if (!Validate(address)) throw new DomainException("Invalid email");
        Address = address;
    }

    public static bool Validate(string email)
    {
        var regexEmail = new Regex(@"^(?("")("".+?""@)|((0-9a-zA-Z)|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }
}