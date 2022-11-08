using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class ProductionOvertimeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OverTimeType",
                schema: "People",
                table: "EmployeeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Production",
                schema: "People",
                table: "EmployeeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredProduction",
                schema: "People",
                table: "EmployeeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Production",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "RequiredProduction",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.AlterColumn<string>(
                name: "OverTimeType",
                schema: "People",
                table: "EmployeeRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
