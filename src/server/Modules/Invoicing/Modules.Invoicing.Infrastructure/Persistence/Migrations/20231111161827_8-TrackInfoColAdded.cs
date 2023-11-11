using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8TrackInfoColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingCompany",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingUrl",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ConfirmedAt",
                schema: "Invoicing",
                table: "OrderLineItem",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConfirmedQty",
                schema: "Invoicing",
                table: "OrderLineItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                schema: "Invoicing",
                table: "OrderLineItem",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingCompany",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TrackingUrl",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                schema: "Invoicing",
                table: "OrderLineItem");

            migrationBuilder.DropColumn(
                name: "ConfirmedQty",
                schema: "Invoicing",
                table: "OrderLineItem");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Invoicing",
                table: "OrderLineItem");
        }
    }
}
