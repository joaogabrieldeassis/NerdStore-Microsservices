using NerdStoreEntripese.WebApp.MVC.Models;

namespace NerdStoreEntripese.WebApp.MVC.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient,
        IConfiguration configuration)
    {
        httpClient.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value!);

        _httpClient = httpClient;
    }

    public async Task<ProductViewModel?> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalog/produtos/{id}");

        TreatErrosResponse(response);

        return await DeserializarObjetoResponse<ProductViewModel>(response);
    }

    public async Task<IEnumerable<ProductViewModel>?> GetAll()
    {
        var response = await _httpClient.GetAsync("/catalog/produtos/");

        TreatErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<ProductViewModel>>(response);
    }
}