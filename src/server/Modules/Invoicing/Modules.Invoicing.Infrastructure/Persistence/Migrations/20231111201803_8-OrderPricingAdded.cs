using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8OrderPricingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentTotalDiscounts",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubtotalPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalLineItemsPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalOutstanding",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTax",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTotalDiscounts",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "SubtotalPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TotalLineItemsPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TotalOutstanding",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TotalTax",
                schema: "Invoicing",
                table: "FulfillmentOrders");
        }
    }
}
