using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class EmployeeExtraColummns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domicile",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EOBINo",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NICPlace",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNo",
                schema: "People",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendanceStatus",
                schema: "People",
                table: "EmployeeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ModificationId",
                schema: "People",
                table: "EmployeeRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OverTimeType",
                schema: "People",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Production",
                schema: "People",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredProduction",
                schema: "People",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodGroup",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Domicile",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EOBINo",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MotherName",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NICPlace",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Qualification",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNo",
                schema: "People",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AttendanceStatus",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "ModificationId",
                schema: "People",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "OverTimeType",
                schema: "People",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Production",
                schema: "People",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "RequiredProduction",
                schema: "People",
                table: "Attendances");
        }
    }
}
