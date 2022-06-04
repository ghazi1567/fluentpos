using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class productnewcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpeningStock",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                schema: "Catalog",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningStock",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Catalog",
                table: "Products");
        }
    }
}
