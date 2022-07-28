using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence.Migrations
{
    public partial class location2added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "location2",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location2",
                schema: "Catalog",
                table: "Products");
        }
    }
}
