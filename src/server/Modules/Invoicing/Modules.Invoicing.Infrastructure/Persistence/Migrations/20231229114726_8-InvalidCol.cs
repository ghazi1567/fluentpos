using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _8InvalidCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalTax",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalShippingCharges",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalReserveAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalReceivable",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalNetAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ToTalCODAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "InvalidCount",
                schema: "Invoicing",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                schema: "Invoicing",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                schema: "Invoicing",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "UpfrontCharges",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "UpfrontAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCharges",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReserveAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetAmountReceivable",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "CODAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "decimal(23,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvalidCount",
                schema: "Invoicing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                schema: "Invoicing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsValid",
                schema: "Invoicing",
                table: "Invoices");

            migrationBuilder.AlterColumn<double>(
                name: "TotalTax",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalShippingCharges",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalReserveAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalReceivable",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalNetAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ToTalCODAmount",
                schema: "Invoicing",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "UpfrontCharges",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "UpfrontAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ShippingCharges",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ReserveAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "NetAmountReceivable",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "NetAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CODAmount",
                schema: "Invoicing",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(23,2)");
        }
    }
}
