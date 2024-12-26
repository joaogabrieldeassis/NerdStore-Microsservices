using FluentValidation.Results;

namespace NerdStoreEnterprise.Core.Messages.Integrations;

public class ResponseMessage(ValidationResult validationResult) : Message
{
    public ValidationResult ValidationResult { get; } = validationResult;
}