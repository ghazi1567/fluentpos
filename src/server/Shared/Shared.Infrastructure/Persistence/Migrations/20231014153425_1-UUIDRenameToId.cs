using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1UUIDRenameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "Application",
                table: "EventLogs",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Application",
                table: "EventLogs",
                newName: "UUID");
        }
    }
}
