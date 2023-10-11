using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "ProductVariant",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "ProductImage",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "Brands",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "BrandExtendedAttributes");
        }
    }
}
