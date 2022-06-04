using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class newcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "CartItemExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "CartItemExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "People",
                table: "CartExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "People",
                table: "CartExtendedAttributes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "CartItemExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "CartItemExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "People",
                table: "CartExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "People",
                table: "CartExtendedAttributes");
        }
    }
}
