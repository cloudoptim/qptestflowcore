using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class Addnewcolumnforwebpagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "WebPage",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "WebPage",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "WebPage",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "WebPage",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 31, 13, 372, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_group_id",
                table: "WebPage",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "fk_webpagegroup_webpage_group_id",
                table: "WebPage",
                column: "group_id",
                principalTable: "WebPageGroup",
                principalColumn: "page_group_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_webpagegroup_webpage_group_id",
                table: "WebPage");

            migrationBuilder.DropIndex(
                name: "IX_WebPage_group_id",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "WebPage");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 24, 49, 100, DateTimeKind.Local).AddTicks(2480));
        }
    }
}
