using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6ExtraColAddedInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                schema: "Inventory",
                table: "StockTransactions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "InventoryItemId",
                schema: "Inventory",
                table: "StockTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Committed",
                schema: "Inventory",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "InventoryItemId",
                schema: "Inventory",
                table: "Stocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "OnHand",
                schema: "Inventory",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Rack",
                schema: "Inventory",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IgnoreRackCheck",
                schema: "Inventory",
                table: "ImportRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Rack",
                schema: "Inventory",
                table: "ImportRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                schema: "Inventory",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "Committed",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "OnHand",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Rack",
                schema: "Inventory",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "IgnoreRackCheck",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.DropColumn(
                name: "Rack",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "Inventory",
                table: "StockTransactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
