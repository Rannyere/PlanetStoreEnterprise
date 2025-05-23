// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSE.Payment.API.Data;

#nullable disable

namespace PSE.Payment.API.Migrations;

    [DbContext(typeof(PaymentDbContext))]
    [Migration("20250310225838_Payment_Initial")]
    partial class Payment_Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PSE.Payment.API.Models.PaymentInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentMehtod")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("PSE.Payment.API.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorizationCode")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("CostTransaction")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DateTransaction")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlagCard")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NSU")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TID")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("PSE.Payment.API.Models.Transaction", b =>
                {
                    b.HasOne("PSE.Payment.API.Models.PaymentInfo", "Payment")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentId")
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("PSE.Payment.API.Models.PaymentInfo", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }