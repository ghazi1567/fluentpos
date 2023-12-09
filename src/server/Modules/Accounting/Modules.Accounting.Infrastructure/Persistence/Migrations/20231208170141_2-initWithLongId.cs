using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2initWithLongId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.CreateTable(
                name: "PayrollRequests",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PayPeriod = table.Column<int>(type: "int", nullable: false),
                    SalaryCalculationFormula = table.Column<int>(type: "int", nullable: false),
                    IgnoreAttendance = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreDeductionForAbsents = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreDeductionForLateComer = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreOvertime = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Logs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollRequestId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PayPeriod = table.Column<int>(type: "int", nullable: false),
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
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalOffDays = table.Column<int>(type: "int", nullable: false),
                    TotalHoliDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    CurrentSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Incentive = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Deduction = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PayableSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PerDaySalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PerHourSalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    TotalDaysInMonth = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPerks",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    IsRecursion = table.Column<bool>(type: "bit", nullable: false),
                    IsRecursionUnLimited = table.Column<bool>(type: "bit", nullable: false),
                    RecursionEndMonth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffecitveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTaxable = table.Column<bool>(type: "bit", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    GlobalPerkType = table.Column<int>(type: "int", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPerks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollTransactions",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    TransactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Earning = table.Column<double>(type: "float", nullable: false),
                    Deduction = table.Column<double>(type: "float", nullable: false),
                    DaysOrHours = table.Column<double>(type: "float", nullable: false),
                    PerDaySalary = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
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

        /// <inheritdoc />
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
                name: "SalaryPerks",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Payrolls",
                schema: "Accounting");
        }
    }
}
