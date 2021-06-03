using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class fixedallownullforupdateddateinorguser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated-date",
                table: "OrgUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 3, 8, 27, 43, 272, DateTimeKind.Local).AddTicks(4950));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated-date",
                table: "OrgUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 25, 7, 53, 34, 728, DateTimeKind.Local).AddTicks(2230));
        }
    }
}
