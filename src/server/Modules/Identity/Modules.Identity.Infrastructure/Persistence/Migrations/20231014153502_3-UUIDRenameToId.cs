using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _3UUIDRenameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UUID",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UUID",
                schema: "Identity",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Identity",
                table: "UserExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Identity",
                table: "UserBranchs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "UserExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "UserBranchs",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                newName: "UUID");

            migrationBuilder.AddColumn<string>(
                name: "UUID",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UUID",
                schema: "Identity",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
