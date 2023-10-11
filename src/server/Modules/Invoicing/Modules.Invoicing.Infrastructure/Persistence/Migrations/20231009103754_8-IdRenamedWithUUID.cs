using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8IdRenamedWithUUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Warehouses",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Transactions",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "SyncLogs",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "PurchaseOrders",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Products",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "POProducts",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Invoicing",
                table: "Orders",
                newName: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "Warehouses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "SyncLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "PurchaseOrders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "POProducts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Invoicing",
                table: "Orders",
                newName: "Id");
        }
    }
}
