using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class productcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "discountFactor",
                schema: "Catalog",
                table: "Products",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "location",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productCode",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discountFactor",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "location",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "productCode",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "quantity",
                schema: "Catalog",
                table: "Products");
        }
    }
}
