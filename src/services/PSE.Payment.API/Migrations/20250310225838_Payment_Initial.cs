using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PSE.Payment.API.Migrations;

/// <inheritdoc />
public partial class Payment_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Payments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentMehtod = table.Column<int>(type: "int", nullable: false),
                TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payments", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuthorizationCode = table.Column<string>(type: "varchar(100)", nullable: true),
                FlagCard = table.Column<string>(type: "varchar(100)", nullable: true),
                DateTransaction = table.Column<DateTime>(type: "datetime2", nullable: true),
                TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CostTransaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                TID = table.Column<string>(type: "varchar(100)", nullable: true),
                NSU = table.Column<string>(type: "varchar(100)", nullable: true),
                PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transactions_Payments_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payments",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_PaymentId",
            table: "Transactions",
            column: "PaymentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "Payments");
    }
}