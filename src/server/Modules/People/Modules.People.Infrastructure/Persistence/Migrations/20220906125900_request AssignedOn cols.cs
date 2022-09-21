using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class requestAssignedOncols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedOn",
                schema: "People",
                table: "EmployeeRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedTo",
                schema: "People",
                table: "EmployeeRequests",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedOn",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                schema: "People",
                table: "EmployeeRequests");
        }
    }
}
