namespace NerdStoreEnterprise.Core.Interfaces;

public interface IUnitOfwork
{
    public Task<bool> CommitAsync();
}