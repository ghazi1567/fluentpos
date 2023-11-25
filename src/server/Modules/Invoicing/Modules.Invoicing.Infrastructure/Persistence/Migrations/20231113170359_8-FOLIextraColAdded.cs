using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8FOLIextraColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StockId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rack",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StockId",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockId",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Invoicing",
                table: "FulfillmentLineItems");

            migrationBuilder.DropColumn(
                name: "Rack",
                schema: "Invoicing",
                table: "FulfillmentLineItems");

            migrationBuilder.DropColumn(
                name: "SKU",
                schema: "Invoicing",
                table: "FulfillmentLineItems");

            migrationBuilder.DropColumn(
                name: "StockId",
                schema: "Invoicing",
                table: "FulfillmentLineItems");
        }
    }
}
