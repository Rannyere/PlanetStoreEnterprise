using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PSE.Cart.API.Migrations;

/// <inheritdoc />
public partial class Cart_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CartCustomers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                VoucherUsage = table.Column<bool>(type: "bit", nullable: false),
                Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                VoucherCode = table.Column<string>(type: "varchar(50)", nullable: true),
                DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                DiscountType = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartCustomers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CartItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "varchar(100)", nullable: true),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Image = table.Column<string>(type: "varchar(100)", nullable: true),
                CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_CartItems_CartCustomers_CartId",
                    column: x => x.CartId,
                    principalTable: "CartCustomers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IDX_Customer",
            table: "CartCustomers",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_CartItems_CartId",
            table: "CartItems",
            column: "CartId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CartItems");

        migrationBuilder.DropTable(
            name: "CartCustomers");
    }
}