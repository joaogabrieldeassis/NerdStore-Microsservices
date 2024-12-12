using Microsoft.AspNetCore.Mvc;
using NerdStoreEntripese.WebApp.MVC.Services;

namespace NerdStoreEntripese.WebApp.MVC.Controllers;

public class CatalogController(ICatalogService catalogService) : MainController
{
    private readonly ICatalogService _catalogService = catalogService;

    [HttpGet]
    [Route("")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
        var produtos = await _catalogService.GetAll();

        return View(produtos);
    }

    [HttpGet]
    [Route("produto-detalhe/{id}")]
    public async Task<IActionResult> ProductDetail(Guid id)
    {
        var produto = await _catalogService.GetById(id);

        return View(produto);
    }
}