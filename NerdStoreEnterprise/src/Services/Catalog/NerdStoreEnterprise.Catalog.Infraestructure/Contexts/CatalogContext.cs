﻿using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.Business.Models;
using NerdStoreEnterprise.Core.Interfaces;

namespace NerdStoreEnterprise.Catalog.Infraestructure.Contexts;

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options), IUnitOfwork
{
    public DbSet<Product> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }

    public async Task<bool> CommitAsync()
    {
        return await base.SaveChangesAsync() > 0;
    }
}