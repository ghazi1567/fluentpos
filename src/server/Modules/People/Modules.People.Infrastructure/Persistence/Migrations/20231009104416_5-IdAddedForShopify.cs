using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _5IdAddedForShopify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "ShiftPlanners",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "RequestApprovals",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "OvertimeRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "EmployeeRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "CartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "CartItemExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "CartExtendedAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "AttendanceLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "ApprovalFlows",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "People",
                table: "ApprovalFlowLevels",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "ShiftPlanners");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "RequestApprovals");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "CartItemExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "CartExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "ApprovalFlows");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "People",
                table: "ApprovalFlowLevels");
        }
    }
}
