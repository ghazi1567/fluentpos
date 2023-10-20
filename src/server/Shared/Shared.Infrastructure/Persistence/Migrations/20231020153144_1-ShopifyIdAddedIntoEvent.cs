using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1ShopifyIdAddedIntoEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShopifyId",
                schema: "Application",
                table: "WebhookEvents",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopifyId",
                schema: "Application",
                table: "WebhookEvents");
        }
    }
}
