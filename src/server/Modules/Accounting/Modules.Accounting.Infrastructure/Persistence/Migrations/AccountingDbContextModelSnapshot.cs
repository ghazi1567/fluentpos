﻿// <auto-generated />
using System;
using FluentPOS.Modules.Accounting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    partial class AccountingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Accounting")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.Payroll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AbsentDays")
                        .HasColumnType("int");

                    b.Property<int>("AllowedOffDays")
                        .HasColumnType("int");

                    b.Property<string>("BankAccountNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankAccountTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankBranchCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EarnedDays")
                        .HasColumnType("int");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<double>("EmployeeSalary")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<double>("NetPay")
                        .HasColumnType("float");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMode")
                        .HasColumnType("int");

                    b.Property<long>("PayrollRequestId")
                        .HasColumnType("bigint");

                    b.Property<int>("PresentDays")
                        .HasColumnType("int");

                    b.Property<int>("RequiredDays")
                        .HasColumnType("int");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalDays")
                        .HasColumnType("int");

                    b.Property<double>("TotalDeduction")
                        .HasColumnType("float");

                    b.Property<double>("TotalEarned")
                        .HasColumnType("float");

                    b.Property<int>("TotalHoliDays")
                        .HasColumnType("int");

                    b.Property<double>("TotalIncentive")
                        .HasColumnType("float");

                    b.Property<int>("TotalOffDays")
                        .HasColumnType("int");

                    b.Property<double>("TotalOvertime")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("leaves")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payrolls", "Accounting");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.PayrollRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IgnoreAttendance")
                        .HasColumnType("bit");

                    b.Property<bool>("IgnoreDeductionForAbsents")
                        .HasColumnType("bit");

                    b.Property<bool>("IgnoreDeductionForLateComer")
                        .HasColumnType("bit");

                    b.Property<bool>("IgnoreOvertime")
                        .HasColumnType("bit");

                    b.Property<string>("Logs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<int>("SalaryCalculationFormula")
                        .HasColumnType("int");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PayrollRequests", "Accounting");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.PayrollTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("DaysOrHours")
                        .HasColumnType("float");

                    b.Property<double>("Deduction")
                        .HasColumnType("float");

                    b.Property<double>("Earning")
                        .HasColumnType("float");

                    b.Property<int>("EntryType")
                        .HasColumnType("int");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<long>("PayrollId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PerDaySalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("TransactionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PayrollId");

                    b.ToTable("PayrollTransactions", "Accounting");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.Salary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("BasicSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("CurrentSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("Deduction")
                        .HasColumnType("decimal(23,2)");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Incentive")
                        .HasColumnType("decimal(23,2)");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PayableSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("PerDaySalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("PerHourSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalDaysInMonth")
                        .HasColumnType("decimal(23,2)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Salaries", "Accounting");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.SalaryPerks", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EffecitveFrom")
                        .HasColumnType("datetime2");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<int>("GlobalPerkType")
                        .HasColumnType("int");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecursion")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecursionUnLimited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTaxable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<double>("Percentage")
                        .HasColumnType("float");

                    b.Property<DateTime?>("RecursionEndMonth")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalaryPerks", "Accounting");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.PayrollTransaction", b =>
                {
                    b.HasOne("FluentPOS.Modules.Accounting.Core.Entities.Payroll", null)
                        .WithMany("Transactions")
                        .HasForeignKey("PayrollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.Payroll", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
