using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8RNOrderTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FulfillmentOrders_Order_InternalOrderId",
                schema: "Invoicing",
                table: "FulfillmentOrders");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "Invoicing",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "Invoicing",
                newName: "Orders",
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

            migrationBuilder.AddForeignKey(
                name: "FK_FulfillmentOrders_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id");

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
                name: "FK_Orders_Addresses_BillingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "BillingAddressId",
                principalSchema: "Invoicing",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders",
                column: "ShippingAddressId",
                principalSchema: "Invoicing",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FulfillmentOrders_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "FulfillmentOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Fulfillments_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "Fulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "LineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_BillingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "Invoicing",
                newName: "Order",
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

            migrationBuilder.AddForeignKey(
                name: "FK_FulfillmentOrders_Order_InternalOrderId",
                schema: "Invoicing",
                table: "FulfillmentOrders",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Order",
                principalColumn: "Id");

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
    }
}
