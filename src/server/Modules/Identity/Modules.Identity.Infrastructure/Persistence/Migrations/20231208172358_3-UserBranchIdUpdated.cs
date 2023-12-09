using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _3UserBranchIdUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "UserBranchs");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                schema: "Identity",
                table: "UserBranchs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                schema: "Identity",
                table: "UserBranchs");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "Identity",
                table: "UserBranchs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
