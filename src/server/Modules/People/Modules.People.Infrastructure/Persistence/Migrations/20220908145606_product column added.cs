using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class productcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalIndex",
                schema: "People",
                table: "RequestApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ApproverId",
                schema: "People",
                table: "RequestApprovals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReportingTo",
                schema: "People",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalIndex",
                schema: "People",
                table: "RequestApprovals");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                schema: "People",
                table: "RequestApprovals");

            migrationBuilder.DropColumn(
                name: "ReportingTo",
                schema: "People",
                table: "Employees");
        }
    }
}
