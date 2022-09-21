using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    public partial class EmployeeIdMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "Identity",
                table: "Users");
        }
    }
}
