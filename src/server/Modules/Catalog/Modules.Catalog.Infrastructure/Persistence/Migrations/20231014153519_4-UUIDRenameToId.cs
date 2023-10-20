using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4UUIDRenameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductUUID",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductUUID",
                schema: "Catalog",
                table: "ProductVariant");

            migrationBuilder.RenameColumn(
                name: "ProductUUID",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "ProductId1");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductUUID",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductId1");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductUUID",
                schema: "Catalog",
                table: "ProductImage",
                newName: "ProductId1");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "ProductImage",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductUUID",
                schema: "Catalog",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId1");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "Brands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId1",
                schema: "Catalog",
                table: "ProductImage",
                column: "ProductId1",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductId1",
                schema: "Catalog",
                table: "ProductVariant",
                column: "ProductId1",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId1",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductId1",
                schema: "Catalog",
                table: "ProductVariant");

            migrationBuilder.RenameColumn(
                name: "ProductId1",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "ProductUUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "UUID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductId1",
                schema: "Catalog",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductUUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Products",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "ProductId1",
                schema: "Catalog",
                table: "ProductImage",
                newName: "ProductUUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductImage",
                newName: "UUID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId1",
                schema: "Catalog",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductUUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Categories",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "Brands",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                newName: "UUID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductUUID",
                schema: "Catalog",
                table: "ProductImage",
                column: "ProductUUID",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "UUID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductUUID",
                schema: "Catalog",
                table: "ProductVariant",
                column: "ProductUUID",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "UUID");
        }
    }
}
