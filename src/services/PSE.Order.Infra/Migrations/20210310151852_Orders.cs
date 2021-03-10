using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSE.Order.Infra.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    DateUsage = table.Column<DateTime>(nullable: true),
                    DateValidity = table.Column<DateTime>(nullable: false),
                    Activ = table.Column<bool>(nullable: false),
                    Usage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<uint>(type: "int unsigned", nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    VoucherId = table.Column<Guid>(nullable: true),
                    VoucherUsage = table.Column<bool>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalValue = table.Column<decimal>(nullable: false),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    Street = table.Column<string>(type: "varchar(100)", nullable: true),
                    Number = table.Column<string>(type: "varchar(100)", nullable: true),
                    Complement = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    Neighborhoodty = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    State = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ValueUnit = table.Column<decimal>(nullable: false),
                    ProductImage = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherId",
                table: "Orders",
                column: "VoucherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Vouchers");
        }
    }
}
