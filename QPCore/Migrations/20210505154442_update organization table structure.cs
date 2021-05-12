using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class updateorganizationtablestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdDate",
                table: "Organization",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Organization",
                newName: "createdby");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createddate",
                table: "Organization",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "createdby",
            //    table: "Organization",
            //    type: "integer",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(string),
            //    oldType: "character varying(255)",
            //    oldMaxLength: 255,
            //    oldNullable: true);

            migrationBuilder.Sql("ALTER TABLE public.\"Organization\" ALTER COLUMN createdby TYPE integer USING (createdby::integer)");

            migrationBuilder.AlterColumn<int>(
                name: "orgid",
                table: "Organization",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "updatedby",
                table: "Organization",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddate",
                table: "Organization",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                columns: new[] { "createdby", "createddate" },
                values: new object[] { 0, new DateTime(2021, 5, 5, 22, 44, 41, 551, DateTimeKind.Local).AddTicks(6116) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updatedby",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "updateddate",
                table: "Organization");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "Organization",
                newName: "createdDate");

            migrationBuilder.RenameColumn(
                name: "createdby",
                table: "Organization",
                newName: "createdBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "Organization",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "Organization",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "orgid",
                table: "Organization",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                columns: new[] { "createdBy", "createdDate" },
                values: new object[] { null, new DateTime(2021, 5, 5, 21, 31, 28, 877, DateTimeKind.Local).AddTicks(9335) });
        }
    }
}
