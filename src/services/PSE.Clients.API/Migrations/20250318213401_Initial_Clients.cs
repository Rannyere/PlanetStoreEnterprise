using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PSE.Clients.API.Migrations;

/// <inheritdoc />
public partial class Initial_Clients : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "varchar(150)", nullable: false),
                Email = table.Column<string>(type: "varchar(254)", nullable: true),
                Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Addresses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Street = table.Column<string>(type: "varchar(200)", nullable: false),
                Number = table.Column<string>(type: "varchar(50)", nullable: false),
                Complement = table.Column<string>(type: "varchar(250)", nullable: true),
                ZipCode = table.Column<string>(type: "varchar(20)", nullable: false),
                Neighborhood = table.Column<string>(type: "varchar(100)", nullable: false),
                City = table.Column<string>(type: "varchar(100)", nullable: false),
                State = table.Column<string>(type: "varchar(50)", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Addresses", x => x.Id);
                table.ForeignKey(
                    name: "FK_Addresses_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customers",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Addresses_CustomerId",
            table: "Addresses",
            column: "CustomerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Addresses");

        migrationBuilder.DropTable(
            name: "Customers");
    }
}