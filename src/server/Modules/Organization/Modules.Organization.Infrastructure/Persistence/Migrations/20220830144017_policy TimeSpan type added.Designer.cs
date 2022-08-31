﻿// <auto-generated />
using System;
using FluentPOS.Modules.Organization.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(OrganizationDbContext))]
    [Migration("20220830144017_policy TimeSpan type added")]
    partial class policyTimeSpantypeadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Org")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Branchs");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("FluentPOS.Modules.Organization.Core.Entities.Designation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Designations");
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

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Organisations");
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

                    b.Property<DateTime?>("CreateaAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DailyOverTime")
                        .HasColumnType("int");

                    b.Property<int>("DailyOverTimeRate")
                        .HasColumnType("int");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EarlyArrivalPolicy")
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

                    b.Property<DateTime>("ShiftEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ShiftStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeoutPolicy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("lateComersPenalty")
                        .HasColumnType("int");

                    b.Property<int>("lateComersPenaltyType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
