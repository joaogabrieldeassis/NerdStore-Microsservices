using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace NerdStoreEnterprise.Identity.Api.Controllers;

public abstract class MainController : ControllerBase
{
    protected ICollection<string> Erros = [];

    protected bool OperacaoValida()
    {
        return Erros.Count > 0;
    }

    protected ActionResult CustomReponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
        return BadRequest(new
        {
            success = false,
            errors = Erros.Select(x => x)
        });
    }

    protected ActionResult CustomReponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifierErrosModelStateInvalid(modelState);
        return CustomReponse();
    }

    protected void NotifierErrosModelStateInvalid(ModelStateDictionary modelState)
    {
        var toTakeErros = modelState.Values.SelectMany(x => x.Errors);
        foreach (var error in toTakeErros)
        {
            var receiveError = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifierErro(receiveError);
        }
    }

    protected void NotifierErro(string message)
    {
        Erros.Add(message);
    }
}