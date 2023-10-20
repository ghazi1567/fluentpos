using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2UUIDRenameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Accounting",
                table: "Salaries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Accounting",
                table: "PayrollTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Accounting",
                table: "Payrolls",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Accounting",
                table: "PayrollRequests",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "Salaries",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollTransactions",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "Payrolls",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Accounting",
                table: "PayrollRequests",
                newName: "UUID");
        }
    }
}
