using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6ExtraColInFile1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                schema: "Inventory",
                table: "ImportRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VariantId",
                schema: "Inventory",
                table: "ImportRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                schema: "Inventory",
                table: "ImportRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.DropColumn(
                name: "VariantId",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                schema: "Inventory",
                table: "ImportRecords");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Inventory",
                table: "ImportRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
