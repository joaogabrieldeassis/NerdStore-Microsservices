namespace NerdStorageEnterprise.Payment.NerdsApi;

public enum TransactionStatus
{
    Authorized = 1,
    Paid,
    Refused,
    Chargedback,
    Cancelled
}