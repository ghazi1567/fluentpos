using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class policyTimeSpantype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftStartTime",
                schema: "Org",
                table: "Policies",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftEndTime",
                schema: "Org",
                table: "Policies",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftStartTime",
                schema: "Org",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftEndTime",
                schema: "Org",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
