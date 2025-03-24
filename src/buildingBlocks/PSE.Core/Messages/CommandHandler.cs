using FluentValidation.Results;
using PSE.Core.Data;
using System.Threading.Tasks;

namespace PSE.Core.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddErrors(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected async Task<ValidationResult> PersistToBase(IUnityOfWork uow)
    {
        if (!await uow.Commit()) AddErrors("There was an error persisting the data");

        return ValidationResult;
    }
}
