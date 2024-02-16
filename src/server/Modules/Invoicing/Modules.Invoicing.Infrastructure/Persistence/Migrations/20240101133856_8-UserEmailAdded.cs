using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "SyncLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "POProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "OrderLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "OperationCity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LineItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Fulfillments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "SyncLogs");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "OrderLogs");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "OperationCity");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Fulfillments");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "FulfillmentLineItems");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Invoicing",
                table: "Addresses");
        }
    }
}
