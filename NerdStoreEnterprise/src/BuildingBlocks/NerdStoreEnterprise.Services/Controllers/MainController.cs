using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;


namespace NerdStoreEnterprise.Services.Controllers;

public abstract class MainController : Controller
{
    protected ICollection<string> Errors = [];

    protected ActionResult CustomResponse(object? result = null)
    {
        if (OperationIsValid())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
    {
        { "Messages", Errors.ToArray() }
    }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddProcessingError(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddProcessingError(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool OperationIsValid()
    {
        return Errors.Count > 0;
    }

    protected void AddProcessingError(string error)
    {
        Errors.Add(error);
    }

    protected void ClearProcessingErrors()
    {
        Errors.Clear();
    }
}
