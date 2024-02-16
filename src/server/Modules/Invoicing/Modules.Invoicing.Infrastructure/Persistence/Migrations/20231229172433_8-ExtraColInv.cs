using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8ExtraColInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "Invoicing",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FulfillmentOrderId",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrderShopifyId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "Invoicing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FulfillmentOrderId",
                schema: "Invoicing",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "OrderShopifyId",
                schema: "Invoicing",
                table: "FulfillmentOrders");
        }
    }
}
