using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class GlobalPerks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "GlobalPerkType",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalPerkType",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.DropColumn(
                name: "IsGlobal",
                schema: "Accounting",
                table: "SalaryPerks");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                schema: "Accounting",
                table: "SalaryPerks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
