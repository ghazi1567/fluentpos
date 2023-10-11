using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "Warehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "Transactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "SyncLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "PurchaseOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "POProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "SyncLogs");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Orders");
        }
    }
}
