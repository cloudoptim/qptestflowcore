using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addtestplantestplantestcasetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestPlan",
                columns: table => new
                {
                    testplan_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    assign_to = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_testplan_testplan_id", x => x.testplan_id);
                    table.ForeignKey(
                        name: "fk_testplan_orguser_assign_to_user_id",
                        column: x => x.assign_to,
                        principalTable: "OrgUsers",
                        principalColumn: "user-id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_testplan_parent_id_testplan_id",
                        column: x => x.parent_id,
                        principalTable: "TestPlan",
                        principalColumn: "testplan_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TestPlanTestCaseAssociation",
                columns: table => new
                {
                    testplan_testcase_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    testplan_id = table.Column<int>(type: "integer", nullable: false),
                    testcase_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_testplantestcaseassociation_id", x => x.testplan_testcase_id);
                    table.ForeignKey(
                        name: "fk_testflow_testplantestcase_testcase_id",
                        column: x => x.testcase_id,
                        principalTable: "TestFlow",
                        principalColumn: "TestFlowId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_testplan_testcase_testplan_id",
                        column: x => x.testplan_id,
                        principalTable: "TestPlan",
                        principalColumn: "testplan_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 15, 17, 43, 8, 712, DateTimeKind.Local).AddTicks(5042));

            migrationBuilder.CreateIndex(
                name: "IX_TestPlan_assign_to",
                table: "TestPlan",
                column: "assign_to");

            migrationBuilder.CreateIndex(
                name: "ix_testplan_name",
                table: "TestPlan",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestPlan_parent_id",
                table: "TestPlan",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_TestPlanTestCaseAssociation_testcase_id",
                table: "TestPlanTestCaseAssociation",
                column: "testcase_id");

            migrationBuilder.CreateIndex(
                name: "IX_TestPlanTestCaseAssociation_testplan_id",
                table: "TestPlanTestCaseAssociation",
                column: "testplan_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestPlanTestCaseAssociation");

            migrationBuilder.DropTable(
                name: "TestPlan");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 14, 23, 24, 25, 780, DateTimeKind.Local).AddTicks(4630));
        }
    }
}
