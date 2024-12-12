using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Catalog.Api.CQRS.Requests;
using NerdStoreEnterprise.Catalog.Api.CQRS.Responses;
using NerdStoreEnterprise.Services.Identity;

namespace NerdStoreEnterprise.Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [AllowAnonymous]
    [HttpGet("catalog/produtos")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _mediator.Send(new ProductAllQuerie());

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [ClaimsAuthorize("Catalog", "Read")]
    [HttpGet("catalog/produtos/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var response = await _mediator.Send(new ProductByIdQuerie(id));

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [ClaimsAuthorize("Catalog", "Create")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreate product)
    {
        try
        {
            var response = await _mediator.Send(product);

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [ClaimsAuthorize("Catalog", "Update")]
    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdate product)
    {
        try
        {
            var response = await _mediator.Send(product);

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}