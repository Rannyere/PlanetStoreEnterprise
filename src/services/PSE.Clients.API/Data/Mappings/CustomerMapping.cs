using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Clients.API.Models;
using PSE.Core.DomainObjects;

namespace PSE.Clients.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.AddressWeb)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressWebMaxLength})");
            });

            // 1 : * => Customer : Adresses
            builder.HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId);

            builder.ToTable("Customers");
        }
    }
}
