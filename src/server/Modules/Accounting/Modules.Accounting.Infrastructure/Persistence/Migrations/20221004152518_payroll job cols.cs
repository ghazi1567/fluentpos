using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class payrolljobcols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndedAt",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Accounting",
                table: "PayrollRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndedAt",
                schema: "Accounting",
                table: "PayrollRequests");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                schema: "Accounting",
                table: "PayrollRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Accounting",
                table: "PayrollRequests");
        }
    }
}
