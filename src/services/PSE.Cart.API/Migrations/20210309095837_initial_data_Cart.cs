using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSE.Cart.API.Migrations
{
    public partial class initial_data_Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    TotalValue = table.Column<decimal>(nullable: false),
                    VoucherUsage = table.Column<bool>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    VoucherCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    DiscountPercentage = table.Column<decimal>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: true),
                    DiscountType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    CartId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_CartCustomers_CartId",
                        column: x => x.CartId,
                        principalTable: "CartCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CartCustomers");
        }
    }
}
