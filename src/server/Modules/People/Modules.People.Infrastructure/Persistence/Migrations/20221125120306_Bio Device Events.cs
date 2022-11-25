using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class BioDeviceEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abnormal",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.DropColumn(
                name: "AttendanceCheckPoint",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.DropColumn(
                name: "AttendanceStatus",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.DropColumn(
                name: "CustomName",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                schema: "People",
                table: "AttendanceLogs",
                newName: "PunchCode");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "People",
                table: "AttendanceLogs",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "HandlingType",
                schema: "People",
                table: "AttendanceLogs",
                newName: "DeviceSerialNo");

            migrationBuilder.RenameColumn(
                name: "Department",
                schema: "People",
                table: "AttendanceLogs",
                newName: "DeviceName");

            migrationBuilder.RenameColumn(
                name: "DataSource",
                schema: "People",
                table: "AttendanceLogs",
                newName: "CardNo");

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendanceDate",
                schema: "People",
                table: "AttendanceLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendanceTime",
                schema: "People",
                table: "AttendanceLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceDate",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.DropColumn(
                name: "AttendanceTime",
                schema: "People",
                table: "AttendanceLogs");

            migrationBuilder.RenameColumn(
                name: "PunchCode",
                schema: "People",
                table: "AttendanceLogs",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "Direction",
                schema: "People",
                table: "AttendanceLogs",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "DeviceSerialNo",
                schema: "People",
                table: "AttendanceLogs",
                newName: "HandlingType");

            migrationBuilder.RenameColumn(
                name: "DeviceName",
                schema: "People",
                table: "AttendanceLogs",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "CardNo",
                schema: "People",
                table: "AttendanceLogs",
                newName: "DataSource");

            migrationBuilder.AddColumn<string>(
                name: "Abnormal",
                schema: "People",
                table: "AttendanceLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendanceCheckPoint",
                schema: "People",
                table: "AttendanceLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendanceStatus",
                schema: "People",
                table: "AttendanceLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomName",
                schema: "People",
                table: "AttendanceLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
