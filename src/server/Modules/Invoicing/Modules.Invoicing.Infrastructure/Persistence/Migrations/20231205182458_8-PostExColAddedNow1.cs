using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8PostExColAddedNow1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupAddress",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupAddressCode",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostexToken",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostexUrl",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupAddress",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "PickupAddressCode",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "PostexToken",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "PostexUrl",
                schema: "Invoicing",
                table: "Warehouses");
        }
    }
}
