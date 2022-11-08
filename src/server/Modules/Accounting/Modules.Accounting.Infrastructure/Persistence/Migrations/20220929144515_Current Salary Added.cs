using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class CurrentSalaryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentSalary",
                schema: "Accounting",
                table: "Salaries",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentSalary",
                schema: "Accounting",
                table: "Salaries");
        }
    }
}
