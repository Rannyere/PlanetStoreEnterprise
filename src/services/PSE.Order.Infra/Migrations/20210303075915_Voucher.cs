using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSE.Order.Infra.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", nullable: false),
                    Percentage = table.Column<decimal>(nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vouchers");
        }
    }
}
