using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class Removeuniquecontrainontestplanname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_testplan_name",
                table: "TestPlan");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 19, 23, 1, 32, 534, DateTimeKind.Local).AddTicks(7555));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 15, 17, 43, 8, 712, DateTimeKind.Local).AddTicks(5042));

            migrationBuilder.CreateIndex(
                name: "ix_testplan_name",
                table: "TestPlan",
                column: "name",
                unique: true);
        }
    }
}
