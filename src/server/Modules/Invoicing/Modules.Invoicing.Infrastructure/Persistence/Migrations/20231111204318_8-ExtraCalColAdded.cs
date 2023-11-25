using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8ExtraCalColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscounts",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscounts",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Invoicing",
                table: "FulfillmentLineItems");
        }
    }
}
