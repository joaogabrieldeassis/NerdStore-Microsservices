using NerdStoreEnterprise.Core.Messages.Integrations;

namespace NerdStoreEnterprise.Payment.Api.Services;

public interface IPaymentService
{
    Task<ResponseMessage> AutorizadPayment(Models.Payment payment);
}
