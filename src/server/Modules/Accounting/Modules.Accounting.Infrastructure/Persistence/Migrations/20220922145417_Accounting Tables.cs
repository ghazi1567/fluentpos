using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    public partial class AccountingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.CreateTable(
                name: "PayrollRequests",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PayPeriod = table.Column<int>(type: "int", nullable: false),
                    SalaryCalculationFormula = table.Column<int>(type: "int", nullable: false),
                    IgnoreAttendance = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreDeductionForAbsents = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreDeductionForLateComer = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreOvertime = table.Column<bool>(type: "bit", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeSalary = table.Column<double>(type: "float", nullable: false),
                    RequiredDays = table.Column<int>(type: "int", nullable: false),
                    EarnedDays = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    PresentDays = table.Column<int>(type: "int", nullable: false),
                    AbsentDays = table.Column<int>(type: "int", nullable: false),
                    leaves = table.Column<int>(type: "int", nullable: false),
                    AllowedOffDays = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    TotalEarned = table.Column<double>(type: "float", nullable: false),
                    TotalIncentive = table.Column<double>(type: "float", nullable: false),
                    TotalDeduction = table.Column<double>(type: "float", nullable: false),
                    TotalOvertime = table.Column<double>(type: "float", nullable: false),
                    NetPay = table.Column<double>(type: "float", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Incentive = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Deduction = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PayableSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PerDaySalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PerHourSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    TotalDaysInMonth = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryIncentiveDeductions",
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
                    table.PrimaryKey("PK_SalaryIncentiveDeductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollTransactions",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    TransactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Earning = table.Column<double>(type: "float", nullable: false),
                    Deduction = table.Column<double>(type: "float", nullable: false),
                    DaysOrHours = table.Column<double>(type: "float", nullable: false),
                    PerDaySalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollTransactions_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalSchema: "Accounting",
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayrollTransactions_PayrollId",
                schema: "Accounting",
                table: "PayrollTransactions",
                column: "PayrollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayrollRequests",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "PayrollTransactions",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Salaries",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "SalaryIncentiveDeductions",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Payrolls",
                schema: "Accounting");
        }
    }
}
