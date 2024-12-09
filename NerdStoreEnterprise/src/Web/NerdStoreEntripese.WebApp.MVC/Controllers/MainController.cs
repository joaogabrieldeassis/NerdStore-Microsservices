using Microsoft.AspNetCore.Mvc;
using NerdStoreEntripese.WebApp.MVC.Models.Responses;

namespace NerdStoreEntripese.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseItHasErros(ResponseRequest response)
    {
        if (response != null && response.Errors.Mensagens.Count > 0)
        {
            foreach (var mensagem in response.Errors.Mensagens)
            {
                ModelState.AddModelError(string.Empty, mensagem);
            }

            return true;
        }

        return false;
    }
}