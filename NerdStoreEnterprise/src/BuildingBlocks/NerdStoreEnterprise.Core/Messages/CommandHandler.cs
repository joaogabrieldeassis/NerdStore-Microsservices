using FluentValidation.Results;

namespace NerdStoreEnterprise.Core.Messages;

public class CommandHandler
{
    protected ValidationResult ValidationResult;

    public CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    public void AddErros(string error)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
    }
}