namespace NerdStoreEnterprise.Payment.Api.Models;

public enum TransactionStatus
{
    Authorized = 1,
    Paid,
    Denied,
    Reversed,
    Canceled
}