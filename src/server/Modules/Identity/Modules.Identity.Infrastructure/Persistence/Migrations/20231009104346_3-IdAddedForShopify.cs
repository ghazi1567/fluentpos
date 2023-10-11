using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _3IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Identity",
                table: "UserExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Identity",
                table: "UserBranchs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Identity",
                table: "UserExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Identity",
                table: "UserBranchs");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Identity",
                table: "RoleExtendedAttributes");
        }
    }
}
