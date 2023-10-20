using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4IdRenamedToShopifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Products",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductImage",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Categories",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Brands",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                newName: "ShopifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "ProductImage",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "Brands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                newName: "Id");
        }
    }
}
