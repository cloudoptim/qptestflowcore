using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class adduniqueconstrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_self_compositewebelement_id",
                table: "CompositeWebElements");

            migrationBuilder.DropIndex(
                name: "IX_CompositeWebElements_page_id",
                table: "CompositeWebElements");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 20, 15, 39, 35, 645, DateTimeKind.Local).AddTicks(8080));

            migrationBuilder.CreateIndex(
                name: "unique_compositewebelement_pageid_parentid_webelementid_command",
                table: "CompositeWebElements",
                columns: new[] { "page_id", "parent_id", "web_element_id", "command" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_self_compositewebelement_id",
                table: "CompositeWebElements",
                column: "parent_id",
                principalTable: "CompositeWebElements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_self_compositewebelement_id",
                table: "CompositeWebElements");

            migrationBuilder.DropIndex(
                name: "unique_compositewebelement_pageid_parentid_webelementid_command",
                table: "CompositeWebElements");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 20, 12, 54, 57, 234, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.CreateIndex(
                name: "IX_CompositeWebElements_page_id",
                table: "CompositeWebElements",
                column: "page_id");

            migrationBuilder.AddForeignKey(
                name: "fk_self_compositewebelement_id",
                table: "CompositeWebElements",
                column: "parent_id",
                principalTable: "CompositeWebElements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
