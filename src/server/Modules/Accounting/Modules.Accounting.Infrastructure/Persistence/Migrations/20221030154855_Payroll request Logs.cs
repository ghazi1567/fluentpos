using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class PayrollrequestLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logs",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logs",
                schema: "Accounting",
                table: "PayrollRequests");

            migrationBuilder.DropColumn(
                name: "Message",
                schema: "Accounting",
                table: "PayrollRequests");
        }
    }
}
