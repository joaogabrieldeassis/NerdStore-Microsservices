using FluentValidation;

namespace NerdStoreEnterprise.Cliente.Application.Commands.Validations;

public class RegisterClientCommandValidation : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome do cliente não foi informado");

        RuleFor(c => c.Cpf)
            .Must(CpfIsValid)
            .WithMessage("O CPF informado não é válido.");

        RuleFor(c => c.Email)
            .Must(EmailIsValid)
            .WithMessage("O e-mail informado não é válido.");
    }

    protected static bool CpfIsValid(string cpf)
    {
        return Core.DomainObjects.ValueObjects.Cpf.Validate(cpf);
    }

    protected static bool EmailIsValid(string email)
    {
        return Core.DomainObjects.ValueObjects.Email.Validate(email);
    }
}