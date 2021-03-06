﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSE.Payment.API.Data;

namespace PSE.Payment.API.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    [Migration("20210430151701_PaymentsInfo")]
    partial class PaymentsInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PSE.Payment.API.Models.PaymentInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PaymentMehtod")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PSE.Payment.API.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AuthorizationCode")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("CostTransaction")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("DateTransaction")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FlagCard")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NSU")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TID")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PSE.Payment.API.Models.Transaction", b =>
                {
                    b.HasOne("PSE.Payment.API.Models.PaymentInfo", "Payment")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
