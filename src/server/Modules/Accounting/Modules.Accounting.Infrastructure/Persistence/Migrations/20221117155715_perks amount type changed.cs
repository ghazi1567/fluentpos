using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class perksamounttypechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Percentage",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
