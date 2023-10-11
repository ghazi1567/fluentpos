using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Accounting",
                table: "Salaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Accounting",
                table: "PayrollTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Accounting",
                table: "Payrolls",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollTransactions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollRequests");
        }
    }
}
