using MediatR;

namespace NerdStoreEnterprise.Catalog.Api.Configurations;

public interface ICommand<TResponse> : IRequest<TResponse> { }
