using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Catalog",
                table: "BrandExtendedAttributes");
        }
    }
}
