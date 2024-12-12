using NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Requests;

public record ProductCreate(string Name,
                             string Description,
                             bool IsActive,
                             decimal Price,
                             DateTime CreatedDate,
                             string Image,
                             int StockQuantity) : ICommand<Guid>;