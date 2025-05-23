using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PSE.Catalog.API.Migrations;

/// <inheritdoc />
public partial class Catalog_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "varchar(250)", nullable: false),
                Description = table.Column<string>(type: "varchar(500)", nullable: false),
                Activ = table.Column<bool>(type: "bit", nullable: false),
                Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                Image = table.Column<string>(type: "varchar(250)", nullable: false),
                QuantityStock = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Products");
    }
}