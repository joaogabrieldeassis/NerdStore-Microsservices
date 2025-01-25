namespace NerdStoreEnterprise.Payment.Api.Models;

public class CreditCard
{
    protected CreditCard() { }

    public CreditCard(string cardName, string cardNumber, string expirationMonthYear, string cvv)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        ExpirationMonthYear = expirationMonthYear;
        CVV = cvv;
    }

    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationMonthYear { get; set; }
    public string CVV { get; set; }
}
