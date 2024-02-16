using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8loadsheetcols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FulfillmentOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                newName: "FulFillmentOrderId");

            migrationBuilder.AddColumn<long>(
                name: "InternalFulFillmentOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InternalOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalFulFillmentOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.DropColumn(
                name: "InternalOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails");

            migrationBuilder.RenameColumn(
                name: "FulFillmentOrderId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                newName: "FulfillmentOrderId");
        }
    }
}
