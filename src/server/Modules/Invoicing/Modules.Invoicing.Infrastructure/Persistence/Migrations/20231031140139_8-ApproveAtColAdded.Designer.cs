﻿// <auto-generated />
using System;
using FluentPOS.Modules.Invoicing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    [Migration("20231031140139_8-ApproveAtColAdded")]
    partial class _8ApproveAtColAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Invoicing")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.InternalAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool?>("Default")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InternalAddress", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.InternalOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("AppId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("ApprovedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("BillingAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrowserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("BuyerAcceptsMarketing")
                        .HasColumnType("bit");

                    b.Property<string>("CancelReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CancelledAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CartToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CheckoutId")
                        .HasColumnType("bigint");

                    b.Property<string>("CheckoutToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ClosedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool?>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CurrentSubtotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrentTotalDiscounts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrentTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrentTotalTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerLocale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("EstimatedTaxes")
                        .HasColumnType("bit");

                    b.Property<string>("FinancialStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FulfillmentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LandingSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("OrderStatusUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("OrderType")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentGatewayNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PoNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresentmentCurrency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ProcessedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ProcessingMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferringSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShippingAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("SourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<decimal?>("SubtotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("TaxExempt")
                        .HasColumnType("bit");

                    b.Property<bool?>("TaxesIncluded")
                        .HasColumnType("bit");

                    b.Property<bool?>("Test")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalDiscounts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalLineItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TotalOutstanding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalShippingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalTipReceived")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("TotalWeight")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("ShippingAddressId");

                    b.ToTable("Orders", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.OrderFulfillment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InternalOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("NotifyCustomer")
                        .HasColumnType("bit");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Receipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipmentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingNumbers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingUrls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("VariantInventoryManagement")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternalOrderId");

                    b.ToTable("OrderFulfillment", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.OrderLineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("FulfillableQuantity")
                        .HasColumnType("int");

                    b.Property<long?>("FulfillmentLineItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("FulfillmentService")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FulfillmentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("GiftCard")
                        .HasColumnType("bit");

                    b.Property<long?>("Grams")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("InternalOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderFulfillmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("PreTaxPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("ProductExists")
                        .HasColumnType("bit");

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("RequiresShipping")
                        .HasColumnType("bit");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Taxable")
                        .HasColumnType("bit");

                    b.Property<string>("TipPaymentGateway")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TipPaymentGatewaySpecified")
                        .HasColumnType("bit");

                    b.Property<string>("TipPaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("VariantId")
                        .HasColumnType("bigint");

                    b.Property<string>("VariantInventoryManagement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VariantTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternalOrderId");

                    b.HasIndex("OrderFulfillmentId");

                    b.ToTable("OrderLineItem", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.POProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("POProducts", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Products", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.PurchaseOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApproveBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ApproveDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.SyncLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RemoteClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("SyncLogs", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("PaymentType")
                        .HasColumnType("tinyint");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TenderedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Transactions", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool?>("Legacy")
                        .HasColumnType("bit");

                    b.Property<string>("LocalizedCountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalizedProvinceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShopifyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses", "Invoicing");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.InternalOrder", b =>
                {
                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.InternalAddress", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId");

                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.InternalAddress", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId");

                    b.Navigation("BillingAddress");

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.OrderFulfillment", b =>
                {
                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.InternalOrder", null)
                        .WithMany("Fulfillments")
                        .HasForeignKey("InternalOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.OrderLineItem", b =>
                {
                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.InternalOrder", null)
                        .WithMany("LineItems")
                        .HasForeignKey("InternalOrderId");

                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.OrderFulfillment", null)
                        .WithMany("LineItems")
                        .HasForeignKey("OrderFulfillmentId");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.POProduct", b =>
                {
                    b.HasOne("FluentPOS.Modules.Invoicing.Core.Entities.PurchaseOrder", null)
                        .WithMany("Products")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.InternalOrder", b =>
                {
                    b.Navigation("Fulfillments");

                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.OrderFulfillment", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("FluentPOS.Modules.Invoicing.Core.Entities.PurchaseOrder", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
