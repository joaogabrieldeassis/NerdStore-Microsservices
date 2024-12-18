namespace NerdStoreEnterprise.Core.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    public IUnitOfwork IUnitOfwork { get; }
}