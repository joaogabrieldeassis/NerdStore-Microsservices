using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using NerdStoreEnterprise.Core.Interfaces;
using NerdStoreEnterprise.Cliente.Domain.Models.ValueObjects;
using NerdStoreEnterprise.Cliente.Domain.Models;

namespace NerdStoreEnterprise.Cliente.Infraestructure.Data;

public class ClientesContext : DbContext, IUnitOfwork
{
    public ClientesContext(DbContextOptions<ClientesContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Client> Clientes { get; set; }
    public DbSet<Address> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
    }

    public async Task<bool> CommitAsync()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        return sucesso;
    }
}