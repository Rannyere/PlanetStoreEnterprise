using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Order.Domain.Orders;

namespace PSE.Order.Infra.Data.Mappings;

public class OrderCustomerMapping : IEntityTypeConfiguration<OrderCustomer>
{
    public void Configure(EntityTypeBuilder<OrderCustomer> builder)
    {
        builder.HasKey(o => o.Id);

        builder.OwnsOne(o => o.Address, a =>
        {
            a.Property(oa => oa.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar(200)")
                .IsRequired();

            a.Property(oa => oa.Number)
                .HasColumnName("Number")
                .HasColumnType("varchar(50)")
                .IsRequired();

            a.Property(oa => oa.Complement)
                .HasColumnName("Complement")
                .HasColumnType("varchar(250)");

            a.Property(oa => oa.Neighborhood)
                .HasColumnName("Neighborhood")
                .HasColumnType("varchar(100)")
                .IsRequired();

            a.Property(oa => oa.ZipCode)
                .HasColumnName("ZipCode")
                .HasColumnType("varchar(20)")
                .IsRequired();

            a.Property(oa => oa.City)
                .HasColumnName("City")
                .HasColumnType("varchar(100)")
                .IsRequired();

            a.Property(oa => oa.State)
                .HasColumnName("State")
                .HasColumnType("varchar(50)")
                .IsRequired();
        });

        builder.Property(p => p.Code)
            .HasColumnName("Code")
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.TotalValue)
            .HasColumnType("decimal(18,2)");

        // 1 : N => OrderCustomer : OrderItems
        builder.HasMany(c => c.OrderItems)
            .WithOne(c => c.OrderCustomer)
            .HasForeignKey(c => c.OrderId);

        builder.ToTable("Orders");
    }
}