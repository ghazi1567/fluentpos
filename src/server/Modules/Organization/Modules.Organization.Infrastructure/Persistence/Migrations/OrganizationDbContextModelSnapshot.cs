﻿// <auto-generated />
using System;
using FluentPOS.Modules.Organization.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(OrganizationDbContext))]
    partial class OrganizationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Org")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HeadOfDepartment")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsGlobalDepartment")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Production")
                        .HasColumnType("int");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Departments", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Designation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Designations", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("JobName")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Schedule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Jobs", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.JobHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Job")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("TriggeredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("JobHistory", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Organisation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Organisations", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AllowedLateMinInMonth")
                        .HasColumnType("int");

                    b.Property<int>("AllowedLateMinutes")
                        .HasColumnType("int");

                    b.Property<int>("AllowedOffDays")
                        .HasColumnType("int");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DailyOverTime")
                        .HasColumnType("int");

                    b.Property<int>("DailyOverTimeRate")
                        .HasColumnType("int");

                    b.Property<int>("DailyWorkingHour")
                        .HasColumnType("int");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EarlyArrivalPolicy")
                        .HasColumnType("int");

                    b.Property<int>("EarnedHourPolicy")
                        .HasColumnType("int");

                    b.Property<int>("ForceTimeout")
                        .HasColumnType("int");

                    b.Property<int>("HolidayOverTime")
                        .HasColumnType("int");

                    b.Property<int>("HolidayOverTimeRate")
                        .HasColumnType("int");

                    b.Property<bool>("IsFriday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMonday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNextDay")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSaturday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSunday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsThursday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWednesday")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<int>("PayslipType")
                        .HasColumnType("int");

                    b.Property<int?>("RequiredWorkingHour")
                        .HasColumnType("int");

                    b.Property<int>("SandwichLeaveCount")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("ShiftEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ShiftStartTime")
                        .HasColumnType("time");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<int>("TimeoutPolicy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("lateComersPenalty")
                        .HasColumnType("int");

                    b.Property<int>("lateComersPenaltyType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Policies", "Org");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopifyAdminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopifyAdminPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopifyUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Stores", "Org");
                });
#pragma warning restore 612, 618
        }
    }
}
