using FluentValidation.Results;
using NerdStoreEnterprise.Core.Messages.Integrations;
using NerdStoreEnterprise.Payment.Api.Facade;
using NerdStoreEnterprise.Payment.Api.Models;

namespace NerdStoreEnterprise.Payment.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentFacade _paymentFacade;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentFacade paymentFacade)
    {
        _paymentFacade = paymentFacade;
    }

    public async Task<ResponseMessage> AutorizadPayment(Models.Payment payment)
    {
        var transaction = await _paymentFacade.AuthorizePayment(payment);

        var validationResult = new ValidationResult();
        if (transaction.Status != TransactionStatus.Authorized)
        {
            validationResult.Errors.Add(new ValidationFailure("Pagamento", "Pagamento recusado,entre em contato com a sua operadora"));

            return new ResponseMessage(validationResult);
        }
        payment.AddTransaction(transaction);
        _paymentRepository.AddPayment(payment);

        if (!await _paymentRepository.IUnitOfwork.CommitAsync())
        {

            validationResult.Errors.Add(new ValidationFailure("Pagamento",
                "Houve um erro ao realizar o pagamento"));

            return new ResponseMessage(validationResult);
        }
        return new ResponseMessage(validationResult);
    }
}