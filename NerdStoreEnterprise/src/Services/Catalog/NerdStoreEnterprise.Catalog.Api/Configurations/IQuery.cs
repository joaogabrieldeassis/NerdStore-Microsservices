using MediatR;

namespace NerdStoreEnterprise.Catalog.Api.Configurations;

public interface IQuery<TResponse> : IRequest<TResponse> { }