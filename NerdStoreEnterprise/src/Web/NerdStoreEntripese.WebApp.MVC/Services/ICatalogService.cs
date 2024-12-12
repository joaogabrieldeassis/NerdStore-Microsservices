using NerdStoreEntripese.WebApp.MVC.Models;
using Refit;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public interface ICatalogService
{
    Task<IEnumerable<ProductViewModel>>? GetAll();
    Task<ProductViewModel?> GetById(Guid id);
}

public interface ICatalogServiceRefit
{
    [Get("/catalog/produtos/")]
    Task<IEnumerable<ProductViewModel>> GetAll();

    [Get("/catalog/produtos/{id}")]
    Task<ProductViewModel> GetById(Guid id);
}