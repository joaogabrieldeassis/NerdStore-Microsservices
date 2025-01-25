
using NerdStorageEnterprise.Payment.NerdsApi;

namespace NerdStoreEnterprise.Payment.Api.Facade;

public class PaymentCrediteCardFacade : IPaymentFacade
{
    private readonly NerdsPagService _nerdsPaySvc;
    public PaymentCrediteCardFacade()
    {
        _nerdsPaySvc = new NerdsPagService("_paymentConfig.DefaultApiKey",
            "_paymentConfig.DefaultEncryptionKey");
    }

    public async Task<Models.Transaction> AuthorizePayment(Models.Payment payment)
    {
        

        var cardHashGen = new CardHash(_nerdsPaySvc)
        {
            CardNumber = payment.CreditCard.CardNumber,
            CardHolderName = payment.CreditCard.CardName,
            CardExpirationDate = payment.CreditCard.ExpirationMonthYear,
            CardCvv = payment.CreditCard.CVV
        };
        var cardHash = cardHashGen.Generate();

        var transaction = new Transaction(_nerdsPaySvc)
        {
            CardHash = cardHash,
            CardNumber = payment.CreditCard.CardNumber,
            CardHolderName = payment.CreditCard.CardName,
            CardExpirationDate = payment.CreditCard.ExpirationMonthYear,
            CardCvv = payment.CreditCard.CVV,
            PaymentMethod = PaymentMethod.CreditCard,
            Amount = payment.Amount
        };

        return ToTransaction(await transaction.AuthorizeCardTransaction());
    }

    public async Task<Transaction> CapturePayment(Transaction transaction)
    {
        var trans = ToTransaction(transaction);

        return ToTransaction(await trans.CaptureCardTransaction());
    }

    public async Task<Transaction> CancelAuthorization(Transaction transaction)
    {

        var trans = ToTransaction(transaction);

        return ToTransaction(await trans.CancelAuthorization());
    }

    public static Transaction ToTransaction(Transaction transaction)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            Status = (TransactionStatus)transaction.Status,
            Amount = transaction.Amount,
            CardBrand = transaction.CardBrand,
            AuthorizationCode = transaction.AuthorizationCode,
            Cost = transaction.Cost,
            TransactionDate = transaction.TransactionDate,
            Nsu = transaction.Nsu,
            Tid = transaction.Tid
        };
    }

    public static Transaction ToTransaction(Transaction transaction, NerdsPayService nerdsPayService)
    {
        return new Transaction(nerdsPayService)
        {
            Status = (TransactionStatus)transaction.Status,
            Amount = transaction.Amount,
            CardBrand = transaction.CardBrand,
            AuthorizationCode = transaction.AuthorizationCode,
            Cost = transaction.Cost,
            Nsu = transaction.Nsu,
            Tid = transaction.Tid
        };
    }

}