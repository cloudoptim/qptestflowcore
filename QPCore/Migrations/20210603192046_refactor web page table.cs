using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class refactorwebpagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdby",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "createddatetime",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "updateddatetime",
                table: "WebPage");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 20, 45, 724, DateTimeKind.Local).AddTicks(4120));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "WebPage",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createddatetime",
                table: "WebPage",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<BitArray>(
                name: "isactive",
                table: "WebPage",
                type: "bit(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                table: "WebPage",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddatetime",
                table: "WebPage",
                type: "date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 3, 23, 24, 21, 549, DateTimeKind.Local).AddTicks(4890));
        }
    }
}
