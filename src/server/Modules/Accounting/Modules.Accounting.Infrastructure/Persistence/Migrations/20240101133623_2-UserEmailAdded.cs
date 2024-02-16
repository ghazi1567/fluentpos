using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Accounting",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Accounting",
                table: "PayrollTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Accounting",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Accounting",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Accounting",
                table: "PayrollTransactions");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Accounting",
                table: "PayrollRequests");
        }
    }
}
