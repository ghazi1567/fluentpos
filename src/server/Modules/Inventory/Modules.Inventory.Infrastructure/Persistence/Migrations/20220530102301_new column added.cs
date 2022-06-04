using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    public partial class newcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Inventory",
                table: "StockTransactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Inventory",
                table: "StockTransactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Inventory",
                table: "Stocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Inventory",
                table: "Stocks",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Inventory",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Inventory",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Inventory",
                table: "Stocks");
        }
    }
}
