using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Data.Mappings;

public class PaymentInfoMapping : IEntityTypeConfiguration<PaymentInfo>
{
    public void Configure(EntityTypeBuilder<PaymentInfo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Ignore(c => c.CreditCard);

        // 1 : N => PaymentInfo : Transaction
        builder.HasMany(c => c.Transactions)
           .WithOne(c => c.Payment)
           .HasForeignKey(c => c.PaymentId);

        builder.ToTable("Payments");
    }
}