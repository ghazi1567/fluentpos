﻿// <auto-generated />
using System;
using FluentPOS.Modules.Accounting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    [Migration("20221030154855_Payroll request Logs")]
    partial class PayrollrequestLogs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Accounting")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.Payroll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EarnedDays")
                        .HasColumnType("int");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("EmployeeSalary")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<double>("NetPay")
                        .HasColumnType("float");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMode")
                        .HasColumnType("int");

                    b.Property<Guid>("PayrollRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PresentDays")
                        .HasColumnType("int");

                    b.Property<int>("RequiredDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalDays")
                        .HasColumnType("int");

                    b.Property<double>("TotalDeduction")
                        .HasColumnType("float");

                    b.Property<double>("TotalEarned")
                        .HasColumnType("float");

                    b.Property<double>("TotalIncentive")
                        .HasColumnType("float");

                    b.Property<double>("TotalOvertime")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("leaves")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payrolls");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.PayrollRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<int>("SalaryCalculationFormula")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PayrollRequests");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.PayrollTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("DaysOrHours")
                        .HasColumnType("float");

                    b.Property<double>("Deduction")
                        .HasColumnType("float");

                    b.Property<double>("Earning")
                        .HasColumnType("float");

                    b.Property<int>("EntryType")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PayrollId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PerDaySalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<string>("TransactionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PayrollId");

                    b.ToTable("PayrollTransactions");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.Salary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("BasicSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("Deduction")
                        .HasColumnType("decimal(23,2)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Incentive")
                        .HasColumnType("decimal(23,2)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PayableSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("PerDaySalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("PerHourSalary")
                        .HasColumnType("decimal(23,2)");

                    b.Property<decimal>("TotalDaysInMonth")
                        .HasColumnType("decimal(23,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("FluentPOS.Modules.Accounting.Core.Entities.SalaryPerks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(23,2)");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EffecitveFrom")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRecursion")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecursionUnLimited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTaxable")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(23,2)");

                    b.Property<DateTime?>("RecursionEndMonth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SalaryPerks");
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
