using FluentValidation.Results;
using MediatR;

namespace NerdStoreEnterprise.Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult>
{
    public Command()
    {
        TimesSpan = DateTime.UtcNow;
    }

    public DateTime TimesSpan { get; private set; }
    public ValidationResult? ValidationResult { get; set; }
    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}