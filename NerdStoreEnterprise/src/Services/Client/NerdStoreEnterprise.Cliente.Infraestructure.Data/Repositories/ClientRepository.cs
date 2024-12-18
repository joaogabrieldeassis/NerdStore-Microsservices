using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Cliente.Domain.Models;
using NerdStoreEnterprise.Cliente.Domain.Models.Interfaces.Repositories;
using NerdStoreEnterprise.Cliente.Infraestructure.Data.Context;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Cliente.Infraestructure.Data.Repositories;

public class ClientRepository(ClientContext context) : IClientRepository
{
    private readonly ClientContext _context = context;

    public IUnitOfwork IUnitOfwork => _context;

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public Task<Client?> GetByCpf(string cpf)
    {
        return _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }

    public void Add(Client cliente)
    {
        _context.Clients.Add(cliente);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}