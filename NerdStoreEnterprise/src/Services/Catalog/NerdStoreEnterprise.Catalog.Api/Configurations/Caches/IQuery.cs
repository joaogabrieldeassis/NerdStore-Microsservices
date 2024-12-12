using MediatR;

namespace NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

public interface IQuery<TResponse> : IRequest<TResponse> { }