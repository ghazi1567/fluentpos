using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8123test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                schema: "Invoicing",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "Invoicing",
                table: "Warehouses",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
               name: "Longitude",
               schema: "Invoicing",
               table: "Warehouses",
               type: "decimal(23,2)",
               nullable: false,
               defaultValue: 0m,
               oldClrType: typeof(string),
               oldType: "nvarchar(max)",
               oldNullable: true);
        }
    }
}
