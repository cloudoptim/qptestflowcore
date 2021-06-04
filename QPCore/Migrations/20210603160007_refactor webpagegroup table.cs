using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class refactorwebpagegrouptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createddatetime",
                table: "WebPageGroup");

            migrationBuilder.RenameColumn(
                name: "groupname",
                table: "WebPageGroup",
                newName: "group_name");

            migrationBuilder.DropColumn(
                name: "updateddatetime",
                table: "WebPageGroup");

            migrationBuilder.RenameColumn(
                name: "versionid",
                table: "WebPageGroup",
                newName: "version_id");

            migrationBuilder.DropColumn(
                name: "updatedby",
                table: "WebPageGroup");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "WebPageGroup",
                newName: "page_group_id");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "WebPageGroup");

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "WebPageGroup",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "page_group_id",
                table: "WebPageGroup",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "WebPageGroup",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "WebPageGroup",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "WebPageGroup",
                type: "timestamp without time zone",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "uq_group_name",
                table: "WebPageGroup",
                column: "group_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "fk_webpagegroup_page_group_id",
                table: "WebPageGroup");

            migrationBuilder.DropIndex(
                name: "uq_group_name",
                table: "WebPageGroup");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "WebPageGroup");

            migrationBuilder.RenameColumn(
                name: "group_name",
                table: "WebPageGroup",
                newName: "groupname");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "WebPageGroup");

            migrationBuilder.RenameColumn(
                name: "version_id",
                table: "WebPageGroup",
                newName: "versionid");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "WebPageGroup",
                newName: "updatedby");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "WebPageGroup",
                newName: "createdby");

            migrationBuilder.RenameColumn(
                name: "page_group_id",
                table: "WebPageGroup",
                newName: "id");

            migrationBuilder.AddColumn<DateTime>(
                name: "createddatetime",
                table: "WebPageGroup",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddatetime",
                table: "WebPageGroup",
                type: "date",
                nullable: true);

            migrationBuilder.Sql("DROP sequence public.\"WebPageGroup_page_group_id_seq\" CASCADE");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 3, 8, 27, 43, 272, DateTimeKind.Local).AddTicks(4950));
        }
    }
}
