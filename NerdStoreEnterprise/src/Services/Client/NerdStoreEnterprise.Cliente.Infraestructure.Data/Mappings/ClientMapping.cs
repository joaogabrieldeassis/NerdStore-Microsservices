using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Cliente.Domain.Models;
using NerdStoreEnterprise.Core.DomainObjects.ValueObjects;

namespace NerdStoreEnterprise.Cliente.Infraestructure.Data.Mappings;

public class ClientMapping : IEntityTypeConfiguration<ClientesContext>
{
    public void Configure(EntityTypeBuilder<ClientesContext> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.OwnsOne(c => c.Cpf, tf =>
        {
            tf.Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
        });

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.AddressMaxLength})");
        });

        builder.HasOne(c => c.Address)
            .WithOne(c => c.Client);

        builder.ToTable("Clientes");
    }
}