using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8ShippingAmountColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalShippingPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalShippingPrice",
                schema: "Invoicing",
                table: "Orders");
        }
    }
}
