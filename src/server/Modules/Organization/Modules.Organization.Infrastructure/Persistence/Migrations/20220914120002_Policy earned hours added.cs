using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class Policyearnedhoursadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EarnedHourPolicy",
                schema: "Org",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EarnedHourPolicy",
                schema: "Org",
                table: "Policies");
        }
    }
}
