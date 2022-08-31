using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Org");

            migrationBuilder.CreateTable(
                name: "Branchs",
                schema: "Org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGlobalDepartment = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadOfDepartment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                schema: "Org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                schema: "Org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                schema: "Org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayslipType = table.Column<int>(type: "int", nullable: false),
                    PayPeriod = table.Column<int>(type: "int", nullable: false),
                    AllowedOffDays = table.Column<int>(type: "int", nullable: false),
                    RequiredWorkingHour = table.Column<int>(type: "int", nullable: true),
                    ShiftStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllowedLateMinutes = table.Column<int>(type: "int", nullable: false),
                    AllowedLateMinInMonth = table.Column<int>(type: "int", nullable: false),
                    EarlyArrivalPolicy = table.Column<int>(type: "int", nullable: false),
                    ForceTimeout = table.Column<int>(type: "int", nullable: false),
                    TimeoutPolicy = table.Column<int>(type: "int", nullable: false),
                    IsMonday = table.Column<bool>(type: "bit", nullable: false),
                    IsTuesday = table.Column<bool>(type: "bit", nullable: false),
                    IsWednesday = table.Column<bool>(type: "bit", nullable: false),
                    IsThursday = table.Column<bool>(type: "bit", nullable: false),
                    IsFriday = table.Column<bool>(type: "bit", nullable: false),
                    IsSaturday = table.Column<bool>(type: "bit", nullable: false),
                    IsSunday = table.Column<bool>(type: "bit", nullable: false),
                    DailyOverTime = table.Column<int>(type: "int", nullable: false),
                    HolidayOverTime = table.Column<int>(type: "int", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branchs",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "Designations",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "Organisations",
                schema: "Org");

            migrationBuilder.DropTable(
                name: "Policies",
                schema: "Org");
        }
    }
}
