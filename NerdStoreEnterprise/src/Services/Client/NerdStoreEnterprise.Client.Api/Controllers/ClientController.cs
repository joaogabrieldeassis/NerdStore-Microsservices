using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Cliente.Application.Commands;
using NerdStoreEnterprise.Core.MediatR;
using NerdStoreEnterprise.Services.Controllers;

namespace NerdStoreEnterprise.Client.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : MainController
{
    private readonly IMediatRHandler _mediatorHandler;

    public ClientController(IMediatRHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> Index()
    {
        var resultado = await _mediatorHandler.SendCommand(
            new RegisterClientCommand(Guid.NewGuid(), "João", "jao@edu.com", "30314299076"));

        return CustomResponse(resultado);
    }
}