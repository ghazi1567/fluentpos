using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class SalaryPerks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryIncentiveDeductions",
                schema: "Accounting");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "Accounting",
                table: "Salaries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SalaryPerks",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    IsRecursion = table.Column<bool>(type: "bit", nullable: false),
                    OneTimeMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRecursionUnLimited = table.Column<bool>(type: "bit", nullable: false),
                    RecursionEndMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPerks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryPerks",
                schema: "Accounting");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "Accounting",
                table: "Salaries");

            migrationBuilder.CreateTable(
                name: "SalaryIncentiveDeductions",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRecursion = table.Column<bool>(type: "bit", nullable: false),
                    IsRecursionUnLimited = table.Column<bool>(type: "bit", nullable: false),
                    OneTimeMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecursionEndMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryIncentiveDeductions", x => x.Id);
                });
        }
    }
}
