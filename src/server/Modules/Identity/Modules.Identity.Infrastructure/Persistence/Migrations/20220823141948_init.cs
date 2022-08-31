using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Identity",
                table: "RoleExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "Identity",
                table: "RoleExtendedAttributes");
        }
    }
}
