using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Order.Domain.Orders;

namespace PSE.Order.Infra.Data.Mappings;

public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(p => p.Value)
            .HasColumnType("decimal(18,2)");

        // 1 : N => OrderCustomer : Payment
        builder.HasOne(c => c.OrderCustomer)
            .WithMany(c => c.OrderItems);

        builder.ToTable("OrderItems");
    }
}