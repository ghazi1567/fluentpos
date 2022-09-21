using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.People.Infrastructure.Persistence.Migrations
{
    public partial class ApprovalFLow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalFlows",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowType = table.Column<int>(type: "int", nullable: false),
                    ApprovalType = table.Column<int>(type: "int", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalFlowLevels",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalFlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovalIndex = table.Column<int>(type: "int", nullable: false),
                    CreateaAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalFlowLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalFlowLevels_ApprovalFlows_ApprovalFlowId",
                        column: x => x.ApprovalFlowId,
                        principalSchema: "People",
                        principalTable: "ApprovalFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalFlowLevels_ApprovalFlowId",
                schema: "People",
                table: "ApprovalFlowLevels",
                column: "ApprovalFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalFlowLevels",
                schema: "People");

            migrationBuilder.DropTable(
                name: "ApprovalFlows",
                schema: "People");
        }
    }
}
