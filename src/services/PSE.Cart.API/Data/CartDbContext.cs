using System;
using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PSE.Cart.API.Models;

namespace PSE.Cart.API.Data
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartCustomer> CartCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CartCustomer>()
                .HasIndex(c => c.CustomerId)
                .HasName("IDX_Customer");

            modelBuilder.Entity<CartCustomer>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(vc => vc.Code)
                        .HasColumnName("VoucherCode")
                        .HasColumnType("varchar(50)");

                    v.Property(vc => vc.DiscountType)
                        .HasColumnName("DiscountType");

                    v.Property(vc => vc.DiscountPercentage)
                        .HasColumnName("DiscountPercentage");

                    v.Property(vc => vc.DiscountValue)
                        .HasColumnName("DiscountValue");
                });

            modelBuilder.Entity<CartCustomer>()
                .HasMany(c => c.Items)
                .WithOne(i => i.CartCustomer)
                .HasForeignKey(c => c.CartId);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
