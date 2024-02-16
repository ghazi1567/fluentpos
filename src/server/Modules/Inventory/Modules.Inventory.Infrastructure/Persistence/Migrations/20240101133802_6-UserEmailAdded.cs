using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Inventory",
                table: "StockTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Inventory",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Inventory",
                table: "InventoryLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Inventory",
                table: "ImportRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Inventory",
                table: "ImportFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Inventory",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Inventory",
                table: "InventoryLevels");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Inventory",
                table: "ImportFiles");
        }
    }
}
