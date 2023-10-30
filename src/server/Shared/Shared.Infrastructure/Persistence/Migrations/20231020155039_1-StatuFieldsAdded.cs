using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1StatuFieldsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "Application",
                table: "WebhookEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Application",
                table: "WebhookEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "Application",
                table: "WebhookEvents",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                schema: "Application",
                table: "WebhookEvents");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Application",
                table: "WebhookEvents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Application",
                table: "WebhookEvents");
        }
    }
}
