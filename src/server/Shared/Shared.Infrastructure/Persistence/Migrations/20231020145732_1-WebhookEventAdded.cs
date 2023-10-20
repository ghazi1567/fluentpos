using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1WebhookEventAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookEvents",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TriggeredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopDomain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEvents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookEvents",
                schema: "Application");
        }
    }
}
