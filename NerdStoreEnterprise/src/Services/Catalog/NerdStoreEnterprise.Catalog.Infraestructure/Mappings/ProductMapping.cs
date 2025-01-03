﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Catalog.Business.Models;

namespace NerdStoreEnterprise.Catalog.Infraestructure.Mappings;

internal class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(500)");

        builder.Property(c => c.Image)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.ToTable("Produtos");
    }
}
