using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Cart.Api.Models;
using FluentValidation.Results;

namespace NerdStoreEnterprise.Cart.Api.Data;

public sealed class CartContext : DbContext
{
    public CartContext(DbContextOptions<CartContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CustomerCart> CustomerCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<CustomerCart>()
            .HasIndex(c => c.CustomerId)
            .HasName("IDX_Customer");

        modelBuilder.Entity<CustomerCart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.CustomerCart)
            .HasForeignKey(c => c.CartId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }
}