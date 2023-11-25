using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8TotalQtyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalQuantity",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalQuantity",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalShippingPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TotalShippingPrice",
                schema: "Invoicing",
                table: "FulfillmentOrders");
        }
    }
}
