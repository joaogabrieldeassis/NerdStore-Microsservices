using MediatR;

namespace NerdStoreEnterprise.Catalog.Api.Configurations.Caches;

public interface ICommand<TResponse> : IRequest<TResponse> { }
