using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence.Migrations
{
    public partial class OrderApproveCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POProducts_PurchaseOrders_PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                schema: "Invoicing",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "Invoicing",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "POReferenceNo",
                schema: "Invoicing",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_POProducts_PurchaseOrders_PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts",
                column: "PurchaseOrderId",
                principalSchema: "Invoicing",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POProducts_PurchaseOrders_PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "POReferenceNo",
                schema: "Invoicing",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_POProducts_PurchaseOrders_PurchaseOrderId",
                schema: "Invoicing",
                table: "POProducts",
                column: "PurchaseOrderId",
                principalSchema: "Invoicing",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
