using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class salaryperksadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OneTimeMonth",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "EffecitveFrom");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "decimal(23,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaxable",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.DropColumn(
                name: "Percentage",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.RenameColumn(
                name: "EffecitveFrom",
                schema: "Accounting",
                table: "SalaryPerks",
                newName: "OneTimeMonth");
        }
    }
}
