using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStoreEnterprise.Order.Domain.Orders;

namespace NerdStoreEnterprise.Order.Infraestructure.Data.Mappings;

public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ProductName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        // 1 : N => Order : Payment
        builder.HasOne(c => c.Order)
            .WithMany(c => c.OrderItems);

        builder.ToTable("OrderItems");
    }
}