﻿// <auto-generated />
using System;
using FluentPOS.Modules.Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20231107110202_6-QtyDatTypeChange")]
    partial class _6QtyDatTypeChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Inventory")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.ImportFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("UploadType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ImportFiles", "Inventory");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.ImportRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IgnoreRackCheck")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ImportFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<string>("Rack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Warehouse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImportFileId");

                    b.ToTable("ImportRecords", "Inventory");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.InternalInventoryLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("Available")
                        .HasColumnType("bigint");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("InventoryItemId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("InventoryLevels", "Inventory");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("AvailableQuantity")
                        .HasColumnType("bigint");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Committed")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("InventoryItemId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("OnHand")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Rack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Stocks", "Inventory");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.StockTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("DiscountFactor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FactorDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("InventoryItemId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("StockTransactions", "Inventory");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.ImportRecord", b =>
                {
                    b.HasOne("FluentPOS.Modules.Inventory.Core.Entities.ImportFile", null)
                        .WithMany("ImportRecords")
                        .HasForeignKey("ImportFileId");
                });

            modelBuilder.Entity("FluentPOS.Modules.Inventory.Core.Entities.ImportFile", b =>
                {
                    b.Navigation("ImportRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
