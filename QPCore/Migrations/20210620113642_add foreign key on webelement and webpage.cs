using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addforeignkeyonwebelementandwebpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 20, 18, 36, 42, 37, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.CreateIndex(
                name: "IX_WebElement_pageid",
                table: "WebElement",
                column: "pageid");

            migrationBuilder.AddForeignKey(
                name: "fk_webpage_webelement_webpageid_pageid",
                table: "WebElement",
                column: "pageid",
                principalTable: "WebPage",
                principalColumn: "page_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_webpage_webelement_webpageid_pageid",
                table: "WebElement");

            migrationBuilder.DropIndex(
                name: "IX_WebElement_pageid",
                table: "WebElement");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 20, 15, 39, 35, 645, DateTimeKind.Local).AddTicks(8080));
        }
    }
}
