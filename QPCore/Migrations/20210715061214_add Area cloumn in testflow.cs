using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addAreacloumnintestflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "TestFlow",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestFlow_AreaId",
                table: "TestFlow",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "fk_testflow_testflowcategory_area_id",
                table: "TestFlow",
                column: "AreaId",
                principalTable: "TestFlowCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_testflow_testflowcategory_area_id",
                table: "TestFlow");

            migrationBuilder.DropIndex(
                name: "IX_TestFlow_AreaId",
                table: "TestFlow");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "TestFlow");
        }
    }
}
