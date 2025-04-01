using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Order.Domain.Vouchers;

namespace PSE.Order.Infra.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DiscountType)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.DiscountPercentage)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.DiscountValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.Code)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Vouchers");
    }
}