using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _7IdRenamedToShopifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Policies",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Jobs",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "JobHistory",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Designations",
                newName: "ShopifyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Org",
                table: "Departments",
                newName: "ShopifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Org",
                table: "Policies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Org",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Org",
                table: "JobHistory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Org",
                table: "Designations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShopifyId",
                schema: "Org",
                table: "Departments",
                newName: "Id");
        }
    }
}
