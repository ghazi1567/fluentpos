using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class policycolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyOverTimeRate",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HolidayOverTimeRate",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "lateComersPenalty",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "lateComersPenaltyType",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyOverTimeRate",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "HolidayOverTimeRate",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "lateComersPenalty",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "lateComersPenaltyType",
                schema: "Org",
                table: "Policies");
        }
    }
}
