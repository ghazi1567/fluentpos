using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class policyextracols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyWorkingHour",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SandwichLeaveCount",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyWorkingHour",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "SandwichLeaveCount",
                schema: "Org",
                table: "Policies");
        }
    }
}
