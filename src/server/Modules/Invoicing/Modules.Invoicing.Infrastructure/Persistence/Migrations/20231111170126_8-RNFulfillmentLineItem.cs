using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8RNFulfillmentLineItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLineItem",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "OrderFulfillment",
                schema: "Invoicing");

            migrationBuilder.CreateTable(
                name: "Fulfillments",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "LineItems",
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
                    ConfirmedQty = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IntenalFulfillmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "Fulfillments",
                schema: "Invoicing");

            migrationBuilder.CreateTable(
                name: "OrderFulfillment",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyCustomer = table.Column<bool>(type: "bit", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Receipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    VariantInventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFulfillment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFulfillment_Orders_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineItem",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ConfirmedQty = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FulfillableQuantity = table.Column<int>(type: "int", nullable: true),
                    FulfillmentLineItemId = table.Column<long>(type: "bigint", nullable: true),
                    FulfillmentService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftCard = table.Column<bool>(type: "bit", nullable: true),
                    Grams = table.Column<long>(type: "bigint", nullable: true),
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderFulfillmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreTaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductExists = table.Column<bool>(type: "bit", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    RequiresShipping = table.Column<bool>(type: "bit", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    Taxable = table.Column<bool>(type: "bit", nullable: true),
                    TipPaymentGateway = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipPaymentGatewaySpecified = table.Column<bool>(type: "bit", nullable: false),
                    TipPaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    VariantId = table.Column<long>(type: "bigint", nullable: true),
                    VariantInventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariantTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
        }
    }
}
