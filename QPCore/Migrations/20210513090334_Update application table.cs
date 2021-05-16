using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class Updateapplicationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "Application",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "Orgid",
                table: "Application",
                newName: "OrgId");

            migrationBuilder.RenameColumn(
                name: "ApplicationName",
                table: "Application",
                newName: "application_name");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Application",
                newName: "application_id");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Application",
                newName: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Application_Orgid",
                table: "Application",
                column: "application_id");

            migrationBuilder.RenameIndex(
                name: "ApplicationId",
                table: "Application",
                newName: "ix_application_id");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "Application",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "Application",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "Application",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "Application",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 13, 16, 3, 34, 113, DateTimeKind.Local).AddTicks(1631));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "OrgId",
                table: "Application",
                newName: "Orgid");

            migrationBuilder.RenameColumn(
                name: "application_name",
                table: "Application",
                newName: "ApplicationName");

            migrationBuilder.RenameColumn(
                name: "application_id",
                table: "Application",
                newName: "ApplicationId");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Application",
                newName: "ClientId");

            migrationBuilder.DropIndex(
                name: "IX_Application_OrgId");

            migrationBuilder.RenameIndex(
                name: "ix_application_id",
                table: "Application",
                newName: "ApplicationId");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "Application",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Application",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Application",
                type: "date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 5, 22, 44, 41, 551, DateTimeKind.Local).AddTicks(6116));
        }
    }
}
