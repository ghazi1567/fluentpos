using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    public partial class newcolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateaAt",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "CreateaAt",
                schema: "Identity",
                table: "RoleExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "RoleExtendedAttributes");
        }
    }
}
