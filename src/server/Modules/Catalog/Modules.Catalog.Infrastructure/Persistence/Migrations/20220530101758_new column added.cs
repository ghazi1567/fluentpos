using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class newcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "ProductExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "CategoryExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Brands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Brands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "BrandExtendedAttributes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "ProductExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "CategoryExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Catalog",
                table: "BrandExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Catalog",
                table: "BrandExtendedAttributes");
        }
    }
}
