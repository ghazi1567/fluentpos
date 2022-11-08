using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class payrolloffdaysholidays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalHoliDays",
                schema: "Accounting",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalOffDays",
                schema: "Accounting",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHoliDays",
                schema: "Accounting",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "TotalOffDays",
                schema: "Accounting",
                table: "Payrolls");
        }
    }
}
