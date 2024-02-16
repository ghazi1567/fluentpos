using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _7UserEmailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "StoreWarehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "JobHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "Org",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "StoreWarehouses");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "Org",
                table: "Departments");
        }
    }
}
