using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _7UUIDRenameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Stores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Policies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Organisations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "JobHistory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Designations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Org",
                table: "Departments",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Stores",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Policies",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Organisations",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Jobs",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "JobHistory",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Designations",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Departments",
                newName: "UUID");
        }
    }
}
