using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addrolecodecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rolecode",
                table: "Roles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 4, 21, 21, 12, 602, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "roleid",
                keyValue: 1,
                column: "rolecode",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "roleid",
                keyValue: 2,
                column: "rolecode",
                value: "USER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rolecode",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 3, 23, 42, 26, 639, DateTimeKind.Local).AddTicks(6453));
        }
    }
}
