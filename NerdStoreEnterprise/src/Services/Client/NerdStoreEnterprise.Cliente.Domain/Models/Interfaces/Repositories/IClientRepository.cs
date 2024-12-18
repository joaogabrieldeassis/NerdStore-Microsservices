using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Cliente.Domain.Models.Interfaces.Repositories;

public interface IClientRepository : IRepository<Client>
{
    void Add(Client customer);

    Task<IEnumerable<Client>> GetAll();
    Task<Client?> GetByCpf(string cpf);
}