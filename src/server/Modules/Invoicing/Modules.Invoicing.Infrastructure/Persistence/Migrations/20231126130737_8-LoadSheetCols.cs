using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8LoadSheetCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddress",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                schema: "Invoicing",
                table: "LoadSheetMains",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "Note",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "PickupAddress",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Invoicing",
                table: "LoadSheetMains");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Invoicing",
                table: "LoadSheetMains");
        }
    }
}
