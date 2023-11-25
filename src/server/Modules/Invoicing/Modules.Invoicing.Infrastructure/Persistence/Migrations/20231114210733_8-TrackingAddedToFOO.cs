using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8TrackingAddedToFOO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingStatus",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingCompany",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingStatus",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingUrl",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingStatus",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TrackingCompany",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TrackingStatus",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "TrackingUrl",
                schema: "Invoicing",
                table: "FulfillmentOrders");
        }
    }
}
