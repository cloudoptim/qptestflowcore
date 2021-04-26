using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addverifiedcolumntoorguser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "verified",
                table: "OrgUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 17, 42, 49, 101, DateTimeKind.Local).AddTicks(2318));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verified",
                table: "OrgUsers");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 17, 26, 43, 914, DateTimeKind.Local).AddTicks(974));
        }
    }
}
