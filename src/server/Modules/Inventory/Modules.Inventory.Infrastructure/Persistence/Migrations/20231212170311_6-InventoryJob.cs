using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6InventoryJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ImportFileId",
                schema: "Inventory",
                table: "InventoryLevels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Inventory",
                table: "InventoryLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdatedOnShopify",
                schema: "Inventory",
                table: "ImportRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportFileId",
                schema: "Inventory",
                table: "InventoryLevels");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Inventory",
                table: "InventoryLevels");

            migrationBuilder.DropColumn(
                name: "IsUpdatedOnShopify",
                schema: "Inventory",
                table: "ImportRecords");
        }
    }
}
