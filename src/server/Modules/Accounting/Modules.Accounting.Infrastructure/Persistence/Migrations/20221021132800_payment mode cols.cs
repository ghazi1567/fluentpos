using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class paymentmodecols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                schema: "Accounting",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountTitle",
                schema: "Accounting",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankBranchCode",
                schema: "Accounting",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                schema: "Accounting",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                schema: "Accounting",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayPeriod",
                schema: "Accounting",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "BankAccountTitle",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "BankBranchCode",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "BankName",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "Month",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "PayPeriod",
                schema: "Accounting",
                table: "Payrolls");
        }
    }
}
