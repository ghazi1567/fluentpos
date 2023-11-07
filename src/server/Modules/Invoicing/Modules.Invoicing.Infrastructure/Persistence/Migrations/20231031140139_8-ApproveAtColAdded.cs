using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8ApproveAtColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillment_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                schema: "Invoicing",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillment_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillment_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillment_Orders_InternalOrderId",
                schema: "Invoicing",
                table: "OrderFulfillment",
                column: "InternalOrderId",
                principalSchema: "Invoicing",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
