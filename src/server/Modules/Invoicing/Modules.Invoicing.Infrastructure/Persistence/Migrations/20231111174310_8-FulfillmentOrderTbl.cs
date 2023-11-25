using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8FulfillmentOrderTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fulfillments_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_InternalAddress_BillingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_InternalAddress_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternalAddress",
                schema: "Invoicing",
                table: "InternalAddress");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "Invoicing",
                newName: "Order",
                newSchema: "Invoicing");

            migrationBuilder.RenameTable(
                name: "InternalAddress",
                schema: "Invoicing",
                newName: "Addresses",
                newSchema: "Invoicing");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShippingAddressId",
                schema: "Invoicing",
                table: "Order",
                newName: "IX_Order_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BillingAddressId",
                schema: "Invoicing",
                table: "Order",
                newName: "IX_Order_BillingAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "Invoicing",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "Invoicing",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FulfillmentOrders",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<long>(type: "bigint", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedLocationId = table.Column<long>(type: "bigint", nullable: true),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulfillmentOrderDestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FulfillAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FulfilledBy = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_FulfillmentOrders_Order_InternalOrderId",
                        column: x => x.InternalOrderId,
                        principalSchema: "Invoicing",
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FulfillmentLineItems",
                schema: "Invoicing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<long>(type: "bigint", nullable: true),
                    FulfillmentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    InventoryItemId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    FulfillableQuantity = table.Column<long>(type: "bigint", nullable: true),
                    VariantId = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedQty = table.Column<long>(type: "bigint", nullable: true),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalFulfillmentOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopifyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Fulfillments_Order_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Order_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Addresses_BillingAddressId",
                schema: "Invoicing",
                table: "Order",
                column: "BillingAddressId",
                principalSchema: "Invoicing",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Addresses_ShippingAddressId",
                schema: "Invoicing",
                table: "Order",
                column: "ShippingAddressId",
                principalSchema: "Invoicing",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fulfillments_Order_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Order_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Addresses_BillingAddressId",
                schema: "Invoicing",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Addresses_ShippingAddressId",
                schema: "Invoicing",
                table: "Order");

            migrationBuilder.DropTable(
                name: "FulfillmentLineItems",
                schema: "Invoicing");

            migrationBuilder.DropTable(
                name: "FulfillmentOrders",
                schema: "Invoicing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "Invoicing",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "Invoicing",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "Invoicing",
                newName: "Orders",
                newSchema: "Invoicing");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Invoicing",
                newName: "InternalAddress",
                newSchema: "Invoicing");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                newName: "IX_Orders_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_BillingAddressId",
                schema: "Invoicing",
                table: "Orders",
                newName: "IX_Orders_BillingAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "Invoicing",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternalAddress",
                schema: "Invoicing",
                table: "InternalAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fulfillments_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id");

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
    }
}
