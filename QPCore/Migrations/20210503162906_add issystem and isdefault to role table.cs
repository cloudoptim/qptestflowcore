using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addissystemandisdefaulttoroletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enabled",
                table: "Roles");

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdefault",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "issystem",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 3, 23, 29, 5, 655, DateTimeKind.Local).AddTicks(400));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isactive",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isdefault",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "issystem",
                table: "Roles");

            migrationBuilder.AddColumn<BitArray>(
                name: "enabled",
                table: "Roles",
                type: "bit(1)",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 23, 17, 17, 226, DateTimeKind.Local).AddTicks(2712));
        }
    }
}
