using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addpasswordresetcolumntoorgusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "passwordreset",
                table: "OrgUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 22, 21, 27, 493, DateTimeKind.Local).AddTicks(1630));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordreset",
                table: "OrgUsers");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 22, 12, 19, 173, DateTimeKind.Local).AddTicks(7472));
        }
    }
}
