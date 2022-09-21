using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class Attendanceactualhours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LateHours",
                schema: "People",
                table: "Attendances",
                newName: "LateMinutes");

            migrationBuilder.AddColumn<double>(
                name: "ActualEarnedHours",
                schema: "People",
                table: "Attendances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualEarnedHours",
                schema: "People",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "LateMinutes",
                schema: "People",
                table: "Attendances",
                newName: "LateHours");
        }
    }
}
