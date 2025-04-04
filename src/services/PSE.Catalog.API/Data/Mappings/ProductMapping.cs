using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Catalog.API.Models;

namespace PSE.Catalog.API.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
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

        builder.ToTable("Products");
    }
}