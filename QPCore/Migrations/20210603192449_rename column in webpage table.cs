using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class renamecolumninwebpagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "groupid",
                table: "WebPage",
                newName: "group_id");

            migrationBuilder.RenameColumn(
                name: "pagename",
                table: "WebPage",
                newName: "page_name");

            migrationBuilder.RenameColumn(
                name: "pageid",
                table: "WebPage",
                newName: "page_id");

            migrationBuilder.AlterColumn<int>(
                name: "page_id",
                table: "WebPage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 24, 49, 100, DateTimeKind.Local).AddTicks(2480));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "WebPage",
                newName: "groupid");

            migrationBuilder.RenameColumn(
                name: "page_name",
                table: "WebPage",
                newName: "pagename");

            migrationBuilder.RenameColumn(
                name: "page_id",
                table: "WebPage",
                newName: "pageid");

            migrationBuilder.AlterColumn<int>(
                name: "pageid",
                table: "WebPage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 20, 45, 724, DateTimeKind.Local).AddTicks(4120));
        }
    }
}
