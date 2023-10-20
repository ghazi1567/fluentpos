using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8OrderRelatedClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                schema: "Invoicing",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Discount",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Tax",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "POReferenceNo",
                schema: "Invoicing",
                table: "Orders",
                newName: "TotalOutstanding");

            migrationBuilder.RenameColumn(
                name: "ApprovedBy",
                schema: "Invoicing",
                table: "Orders",
                newName: "Token");

            migrationBuilder.AddColumn<long>(
                name: "AppId",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillingAddressId",
                schema: "Invoicing",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrowserIp",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BuyerAcceptsMarketing",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CancelledAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartToken",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CheckoutId",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckoutToken",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ClosedAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentSubtotalPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentTotalDiscounts",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentTotalPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentTotalTax",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerLocale",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstimatedTaxes",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinancialStatus",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FulfillmentStatus",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandingSite",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                schema: "Invoicing",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                schema: "Invoicing",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatusUrl",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGatewayNames",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoNumber",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentmentCurrency",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ProcessedAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessingMethod",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferringSite",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceName",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubtotalPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TaxExempt",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TaxesIncluded",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Test",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscounts",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalLineItemsPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTax",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTipReceived",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalWeight",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "Invoicing",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InternalAddress",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderFulfillment",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFulfillment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFulfillment_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderLineItem",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FulfillableQuantity = table.Column<int>(type: "int", nullable: true),
                    FulfillmentService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grams = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VariantInventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductExists = table.Column<bool>(type: "bit", nullable: true),
                    PreTaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FulfillmentLineItemId = table.Column<long>(type: "bigint", nullable: true),
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderFulfillmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineItem_OrderFulfillment_OrderFulfillmentId",
                        column: x => x.OrderFulfillmentId,
                        principalSchema: "Invoicing",
                        principalTable: "OrderFulfillment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLineItem_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillment_InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment",
                column: "InternalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItem_InternalOrderId",
                schema: "Invoicing",
                table: "OrderLineItem",
                column: "InternalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItem_OrderFulfillmentId",
                schema: "Invoicing",
                table: "OrderLineItem",
                column: "OrderFulfillmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_InternalAddress_BillingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "BillingAddressId",
                principalSchema: "Invoicing",
                principalTable: "InternalAddress",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_InternalAddress_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "ShippingAddressId",
                principalSchema: "Invoicing",
                principalTable: "InternalAddress",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_InternalAddress_BillingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_InternalAddress_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "InternalAddress",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "OrderLineItem",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "OrderFulfillment",
                schema: "Invoicing");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BrowserIp",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerAcceptsMarketing",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CancelReason",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartToken",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CheckoutToken",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrentSubtotalPrice",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrentTotalDiscounts",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrentTotalPrice",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrentTotalTax",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerLocale",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EstimatedTaxes",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinancialStatus",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FulfillmentStatus",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LandingSite",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Number",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusUrl",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentGatewayNames",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PoNumber",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PresentmentCurrency",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProcessingMethod",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReferringSite",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SourceName",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubtotalPrice",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TaxExempt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TaxesIncluded",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Test",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalDiscounts",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalLineItemsPrice",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalTax",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalTipReceived",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalOutstanding",
                schema: "Invoicing",
                table: "Orders",
                newName: "POReferenceNo");

            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "Invoicing",
                table: "Orders",
                newName: "ApprovedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                schema: "Invoicing",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                schema: "Invoicing",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                schema: "Invoicing",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                schema: "Invoicing",
                table: "Products",
                column: "OrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
