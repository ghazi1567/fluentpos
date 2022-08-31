using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class EmployeeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "CustomerExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "CartItemExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "CartItemExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "People",
                table: "CartExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "People",
                table: "CartExtendedAttributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PunchCode = table.Column<int>(type: "int", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowManualAttendance = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnicNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnicIssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CnicExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "People");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "CustomerExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "CartItemExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "CartItemExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "People",
                table: "CartExtendedAttributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "People",
                table: "CartExtendedAttributes");
        }
    }
}
