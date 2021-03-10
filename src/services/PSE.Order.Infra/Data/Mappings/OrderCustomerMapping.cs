using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Order.Domain.Orders;

namespace PSE.Order.Infra.Data.Mappings
{
    public class OrderCustomerMapping : IEntityTypeConfiguration<OrderCustomer>
    {
        public void Configure(EntityTypeBuilder<OrderCustomer> builder)
        {
            builder.HasKey(o => o.Id);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(oa => oa.Street)
                    .HasColumnName("Street");

                a.Property(oa => oa.Number)
                    .HasColumnName("Number");

                a.Property(oa => oa.Complement)
                    .HasColumnName("Complement");

                a.Property(oa => oa.Neighborhoodty)
                    .HasColumnName("Neighborhoodty");

                a.Property(oa => oa.ZipCode)
                    .HasColumnName("ZipCode");

                a.Property(oa => oa.City)
                    .HasColumnName("City");

                a.Property(oa => oa.State)
                    .HasColumnName("State");
            });

            builder.Property(c => c.Code)
                .HasColumnName("Code")
                .HasColumnType("int unsigned");
                //.HasDefaultValueSql("NEXT VALUE FOR MySequence");

            // 1 : N => OrderCustomer : OrderItems
            builder.HasMany(c => c.OrderItems)
                .WithOne(c => c.OrderCustomer)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Orders");
        }
    }
}
