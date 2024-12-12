using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Catalog.Api.CQRS.Responses;

namespace NerdStoreEnterprise.Catalog.Api.Controllers;

[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _mediator.Send(new ProductAllResponse());

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}