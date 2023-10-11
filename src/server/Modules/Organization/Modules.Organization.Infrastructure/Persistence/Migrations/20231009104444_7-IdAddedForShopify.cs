using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _7IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Org",
                table: "Policies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Org",
                table: "Jobs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Org",
                table: "JobHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Org",
                table: "Designations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "Org",
                table: "Departments",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Org",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Org",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Org",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Org",
                table: "Departments");
        }
    }
}
