using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Core.Interfaces;
using NerdStoreEnterprise.Cliente.Domain.Models.ValueObjects;
using NerdStoreEnterprise.Cliente.Domain.Models;
using NerdStoreEnterprise.Core.MediatR;
using NerdStoreEnterprise.Core.DomainObjects;
using NerdStoreEnterprise.Core.Messages;
using FluentValidation.Results;

namespace NerdStoreEnterprise.Cliente.Infraestructure.Data.Context;

public class ClientContext : DbContext, IUnitOfwork
{
    private readonly IMediatRHandler _mediatorHandler;

    public ClientContext(DbContextOptions<ClientContext> options, IMediatRHandler mediatorHandler)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
    }

    public async Task<bool> CommitAsync()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        if (sucesso) await _mediatorHandler.PublishEvent(this);
        return sucesso;
    }

    
}

public static class MediatorExtension
{
    public static async Task PublishEvent<T>(this IMediatRHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}