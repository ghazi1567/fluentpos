using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _5IdRenameWithUUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "ShiftPlanners",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "RequestApprovals",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "OvertimeRequests",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "Employees",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "EmployeeRequests",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "Customers",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "CustomerExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "Carts",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "CartItems",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "CartItemExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "CartExtendedAttributes",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "Attendances",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "AttendanceLogs",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "ApprovalFlows",
                newName: "UUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "People",
                table: "ApprovalFlowLevels",
                newName: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "ShiftPlanners",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "RequestApprovals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "OvertimeRequests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "EmployeeRequests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "CustomerExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "Carts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "CartItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "CartItemExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "CartExtendedAttributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "Attendances",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "AttendanceLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "ApprovalFlows",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UUID",
                schema: "People",
                table: "ApprovalFlowLevels",
                newName: "Id");
        }
    }
}
