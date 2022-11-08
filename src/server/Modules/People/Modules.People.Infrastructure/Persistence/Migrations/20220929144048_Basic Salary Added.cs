using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class BasicSalaryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BasicSalary",
                schema: "People",
                table: "Employees",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AttendanceStatus",
                schema: "People",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicSalary",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AttendanceStatus",
                schema: "People",
                table: "Attendances");
        }
    }
}
