using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class renameprimarykeyonwebpagegroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "fk_webpagegroup_page_group_id",
                table: "WebPageGroup");

            migrationBuilder.AddPrimaryKey(
                name: "pk_webpagegroup_page_group_id",
                table: "WebPageGroup",
                column: "page_group_id");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 3, 23, 24, 21, 549, DateTimeKind.Local).AddTicks(4890));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_webpagegroup_page_group_id",
                table: "WebPageGroup");

            migrationBuilder.AddPrimaryKey(
                name: "fk_webpagegroup_page_group_id",
                table: "WebPageGroup",
                column: "page_group_id");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 3, 23, 0, 6, 430, DateTimeKind.Local).AddTicks(1780));
        }
    }
}
