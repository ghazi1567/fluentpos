using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _3IdRenamedToShopifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "UserExtendedAttributes",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "UserBranchs",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                newName: "ShopifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Identity",
                table: "UserExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Identity",
                table: "UserBranchs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Identity",
                table: "RoleExtendedAttributes",
                newName: "Id");
        }
    }
}
