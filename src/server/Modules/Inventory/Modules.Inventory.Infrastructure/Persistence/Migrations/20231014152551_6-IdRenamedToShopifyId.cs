using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6IdRenamedToShopifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Inventory",
                table: "StockTransactions",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Inventory",
                table: "Stocks",
                newName: "ShopifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Inventory",
                table: "StockTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Inventory",
                table: "Stocks",
                newName: "Id");
        }
    }
}
