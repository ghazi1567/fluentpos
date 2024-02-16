using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _3UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Identity",
                table: "UserBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Identity",
                table: "UserBranchs");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Identity",
                table: "RoleExtendedAttributes");
        }
    }
}
