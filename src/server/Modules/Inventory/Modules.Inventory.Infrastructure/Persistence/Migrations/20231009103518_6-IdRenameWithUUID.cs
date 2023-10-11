using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6IdRenameWithUUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Inventory",
                table: "StockTransactions",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Inventory",
                table: "Stocks",
                newName: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Inventory",
                table: "StockTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Inventory",
                table: "Stocks",
                newName: "Id");
        }
    }
}
