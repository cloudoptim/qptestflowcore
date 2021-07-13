using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addworkitemandworkitemtestcaseassoctable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    azure_workitem_id = table.Column<int>(type: "integer", nullable: false),
                    azure_feature_id = table.Column<int>(type: "integer", nullable: false),
                    azure_feature_name = table.Column<string>(type: "text", nullable: true),
                    azure_workitem_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workitem_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemTestcaseAssocs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    workitem_id = table.Column<int>(type: "integer", nullable: false),
                    testflow_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workitem_testcase_assoc_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_testflow_workitemtestcaseassoc_testflow_id",
                        column: x => x.testflow_id,
                        principalTable: "TestFlow",
                        principalColumn: "TestFlowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workitem_workitemtestcaseassoc_workitem_id",
                        column: x => x.workitem_id,
                        principalTable: "WorkItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 7, 10, 17, 25, 54, 663, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemTestcaseAssocs_testflow_id",
                table: "WorkItemTestcaseAssocs",
                column: "testflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemTestcaseAssocs_workitem_id",
                table: "WorkItemTestcaseAssocs",
                column: "workitem_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItemTestcaseAssocs");

            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 20, 18, 36, 42, 37, DateTimeKind.Local).AddTicks(7340));
        }
    }
}
