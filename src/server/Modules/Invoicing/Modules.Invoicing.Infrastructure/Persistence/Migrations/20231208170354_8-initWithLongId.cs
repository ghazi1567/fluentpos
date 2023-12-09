using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8initWithLongId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Invoicing");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoadSheetMains",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalOrder = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadSheetMains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCity",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanPickup = table.Column<bool>(type: "bit", nullable: false),
                    CanDeliver = table.Column<bool>(type: "bit", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLogs",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalOrderId = table.Column<long>(type: "bigint", nullable: false),
                    FulfillmentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    LogDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproveBy = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncLogs",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemoteClientId = table.Column<long>(type: "bigint", nullable: false),
                    EntryId = table.Column<long>(type: "bigint", nullable: false),
                    EntryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentType = table.Column<byte>(type: "tinyint", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    TenderedAmount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Legacy = table.Column<bool>(type: "bit", nullable: true),
                    LocalizedCountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalizedProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddressCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostexToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostexUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderType = table.Column<byte>(type: "tinyint", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    AppId = table.Column<long>(type: "bigint", nullable: true),
                    BrowserIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerAcceptsMarketing = table.Column<bool>(type: "bit", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CartToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckoutToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckoutId = table.Column<long>(type: "bigint", nullable: true),
                    ClosedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerLocale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandingSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    OrderStatusUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentGatewayNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProcessingMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferringSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddressId = table.Column<long>(type: "bigint", nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxesIncluded = table.Column<bool>(type: "bit", nullable: true),
                    Test = table.Column<bool>(type: "bit", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTipReceived = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalWeight = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PresentmentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedTaxes = table.Column<bool>(type: "bit", nullable: true),
                    CurrentSubtotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    CurrentTotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    CurrentTotalTax = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxExempt = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQuantity = table.Column<long>(type: "bigint", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    TotalLineItemsPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    CurrentTotalDiscounts = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalDiscounts = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    SubtotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalShippingPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalOutstanding = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalSchema: "Invoicing",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoadSheetDetails",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQuantity = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    FulfillmentOrderId = table.Column<long>(type: "bigint", nullable: false),
                    LoadSheetMainId = table.Column<long>(type: "bigint", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadSheetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoadSheetDetails_LoadSheetMains_LoadSheetMainId",
                        column: x => x.LoadSheetMainId,
                        principalSchema: "Invoicing",
                        principalTable: "LoadSheetMains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "POProducts",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(23,2)", nullable: false),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POProducts_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FulfillmentOrders",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<long>(type: "bigint", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    InternalOrderId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedLocationId = table.Column<long>(type: "bigint", nullable: true),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentOrderDestinationId = table.Column<long>(type: "bigint", nullable: true),
                    FulfillAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FulfilledBy = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    TotalQuantity = table.Column<long>(type: "bigint", nullable: true),
                    OrderType = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true),
                    OrderStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    TotalLineItemsPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    CurrentTotalDiscounts = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalDiscounts = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    SubtotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalShippingPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    TotalOutstanding = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FulfillmentOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FulfillmentOrders_Addresses_FulfillmentOrderDestinationId",
                        column: x => x.FulfillmentOrderDestinationId,
                        principalSchema: "Invoicing",
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FulfillmentOrders_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fulfillments",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    Receipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyCustomer = table.Column<bool>(type: "bit", nullable: true),
                    TrackingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariantInventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalOrderId = table.Column<long>(type: "bigint", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fulfillments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fulfillments_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FulfillmentLineItems",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<long>(type: "bigint", nullable: true),
                    FulfillmentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    InventoryItemId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    FulfillableQuantity = table.Column<long>(type: "bigint", nullable: true),
                    VariantId = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedQty = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    InternalFulfillmentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FulfillmentLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FulfillmentLineItems_FulfillmentOrders_InternalFulfillmentOrderId",
                        column: x => x.InternalFulfillmentOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "FulfillmentOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FulfillableQuantity = table.Column<int>(type: "int", nullable: true),
                    FulfillmentService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grams = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    RequiresShipping = table.Column<bool>(type: "bit", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariantId = table.Column<long>(type: "bigint", nullable: true),
                    VariantTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftCard = table.Column<bool>(type: "bit", nullable: true),
                    Taxable = table.Column<bool>(type: "bit", nullable: true),
                    TipPaymentGateway = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipPaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipPaymentGatewaySpecified = table.Column<bool>(type: "bit", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    VariantInventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductExists = table.Column<bool>(type: "bit", nullable: true),
                    PreTaxPrice = table.Column<decimal>(type: "decimal(23,2)", nullable: true),
                    FulfillmentLineItemId = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedQty = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    IntenalFulfillmentId = table.Column<long>(type: "bigint", nullable: true),
                    InternalOrderId = table.Column<long>(type: "bigint", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_Fulfillments_IntenalFulfillmentId",
                        column: x => x.IntenalFulfillmentId,
                        principalSchema: "Invoicing",
                        principalTable: "Fulfillments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LineItems_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FulfillmentLineItems_InternalFulfillmentOrderId",
                schema: "Invoicing",
                table: "FulfillmentLineItems",
                column: "InternalFulfillmentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FulfillmentOrders_FulfillmentOrderDestinationId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                column: "FulfillmentOrderDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FulfillmentOrders_InternalOrderId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                column: "InternalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Fulfillments_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments",
                column: "InternalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_IntenalFulfillmentId",
                schema: "Invoicing",
                table: "LineItems",
                column: "IntenalFulfillmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems",
                column: "InternalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadSheetDetails_LoadSheetMainId",
                schema: "Invoicing",
                table: "LoadSheetDetails",
                column: "LoadSheetMainId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_POProducts_PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts",
                column: "PurchaseOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FulfillmentLineItems",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "LineItems",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "LoadSheetDetails",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "OperationCity",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "OrderLogs",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "POProducts",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "SyncLogs",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "FulfillmentOrders",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Fulfillments",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "LoadSheetMains",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "PurchaseOrders",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Invoicing");
        }
    }
}
