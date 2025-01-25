using NerdStorageEnterprise.Payment.NerdsApi;

namespace NerdStoreEnterprise.Payment.Api.Facade;

public interface IPaymentFacade
{
    Task<Models.Transaction> AuthorizePayment(Models.Payment payment);
}
