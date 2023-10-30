using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8WarehouseColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "Invoicing",
                table: "Warehouses",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Legacy",
                schema: "Invoicing",
                table: "Warehouses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalizedCountryName",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalizedProvinceName",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Address2",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryName",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Legacy",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LocalizedCountryName",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LocalizedProvinceName",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Zip",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "Invoicing",
                table: "Warehouses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
