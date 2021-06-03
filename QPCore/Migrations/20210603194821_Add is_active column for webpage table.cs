using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class Addis_activecolumnforwebpagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "WebPage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 48, 20, 856, DateTimeKind.Local).AddTicks(580));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "WebPage");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 31, 13, 372, DateTimeKind.Local).AddTicks(5300));
        }
    }
}
