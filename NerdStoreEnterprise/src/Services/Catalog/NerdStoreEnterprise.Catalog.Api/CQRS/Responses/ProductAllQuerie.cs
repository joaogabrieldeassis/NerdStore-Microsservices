using NerdStoreEnterprise.Catalog.Api.Configurations.Caches;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Api.CQRS.Responses;

public record ProductAllQuerie : IQuery<IEnumerable<Product>>;