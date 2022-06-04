using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    public partial class newcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Warehouses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Warehouses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "POProducts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "POProducts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SyncLogs",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoteClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncLogs",
                schema: "Invoicing");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Invoicing",
                table: "Orders");
        }
    }
}
