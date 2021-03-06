﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSE.Cart.API.Data;

namespace PSE.Cart.API.Migrations
{
    [DbContext(typeof(CartDbContext))]
    [Migration("20210309095837_initial_data_Cart")]
    partial class initial_data_Cart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PSE.Cart.API.Models.CartCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("VoucherUsage")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasName("IDX_Customer");

                    b.ToTable("CartCustomers");
                });

            modelBuilder.Entity("PSE.Cart.API.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CartId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("PSE.Cart.API.Models.CartCustomer", b =>
                {
                    b.OwnsOne("PSE.Cart.API.Models.Voucher", "Voucher", b1 =>
                        {
                            b1.Property<Guid>("CartCustomerId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Code")
                                .HasColumnName("VoucherCode")
                                .HasColumnType("varchar(50)");

                            b1.Property<decimal?>("DiscountPercentage")
                                .HasColumnName("DiscountPercentage")
                                .HasColumnType("decimal(65,30)");

                            b1.Property<int>("DiscountType")
                                .HasColumnName("DiscountType")
                                .HasColumnType("int");

                            b1.Property<decimal?>("DiscountValue")
                                .HasColumnName("DiscountValue")
                                .HasColumnType("decimal(65,30)");

                            b1.HasKey("CartCustomerId");

                            b1.ToTable("CartCustomers");

                            b1.WithOwner()
                                .HasForeignKey("CartCustomerId");
                        });
                });

            modelBuilder.Entity("PSE.Cart.API.Models.CartItem", b =>
                {
                    b.HasOne("PSE.Cart.API.Models.CartCustomer", "CartCustomer")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
