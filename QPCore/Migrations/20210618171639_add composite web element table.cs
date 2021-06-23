using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addcompositewebelementtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompositeWebElements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    page_id = table.Column<int>(type: "integer", nullable: false),
                    is_composite = table.Column<bool>(type: "boolean", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    command = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    web_element_id = table.Column<int>(type: "integer", nullable: true),
                    element_alias_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_compositewebelement_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_self_compositewebelement_id",
                        column: x => x.parent_id,
                        principalTable: "CompositeWebElements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_webelement_compositewebelement_id_webelementid",
                        column: x => x.web_element_id,
                        principalTable: "WebElement",
                        principalColumn: "elementid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_webpage_compositewebelement_id_pageid",
                        column: x => x.page_id,
                        principalTable: "WebPage",
                        principalColumn: "page_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 19, 0, 16, 37, 648, DateTimeKind.Local).AddTicks(1160));

            migrationBuilder.CreateIndex(
                name: "IX_CompositeWebElements_page_id",
                table: "CompositeWebElements",
                column: "page_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompositeWebElements_parent_id",
                table: "CompositeWebElements",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompositeWebElements_web_element_id",
                table: "CompositeWebElements",
                column: "web_element_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositeWebElements");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 10, 12, 6, 32, 502, DateTimeKind.Local).AddTicks(2610));
        }
    }
}
