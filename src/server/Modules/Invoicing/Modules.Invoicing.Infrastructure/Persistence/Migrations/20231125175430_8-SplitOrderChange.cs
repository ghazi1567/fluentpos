using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8SplitOrderChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "Invoicing",
                table: "Warehouses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "Invoicing",
                table: "Warehouses",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
