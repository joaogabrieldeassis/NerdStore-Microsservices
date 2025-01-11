using FluentValidation.Results;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Core.Messages;

public class CommandHandler
{
    protected ValidationResult ValidationResult;

    public CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }
    
    protected async Task<ValidationResult> PersistData(IUnitOfwork uow)
    {
        if (!await uow.CommitAsync()) AddErros("There was an error persisting the data");

        return ValidationResult;
    }

    public void AddErros(string error)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
    }
}