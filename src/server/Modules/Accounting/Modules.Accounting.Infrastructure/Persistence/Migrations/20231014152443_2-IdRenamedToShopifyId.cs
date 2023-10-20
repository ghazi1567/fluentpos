using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2IdRenamedToShopifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "Salaries",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollTransactions",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "Payrolls",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollRequests",
                newName: "ShopifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Accounting",
                table: "Salaries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Accounting",
                table: "PayrollTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Accounting",
                table: "Payrolls",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Accounting",
                table: "PayrollRequests",
                newName: "Id");
        }
    }
}
