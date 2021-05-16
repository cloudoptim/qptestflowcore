using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class Updateuserroletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "clientroleassoc",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "enabled",
                table: "UserRoles");

            migrationBuilder.AlterColumn<int>(
                name: "clientroleassoc",
                table: "UserRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "UserRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_userrole_clientroleassoc",
                table: "UserRoles",
                column: "clientroleassoc");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 5, 21, 18, 46, 119, DateTimeKind.Local).AddTicks(2000));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_userrole_clientroleassoc",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "UserRoles");

            migrationBuilder.AlterColumn<int>(
                name: "clientroleassoc",
                table: "UserRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<BitArray>(
                name: "enabled",
                table: "UserRoles",
                type: "bit(1)",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "clientroleassoc",
                table: "UserRoles",
                column: "roleid");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 4, 21, 21, 12, 602, DateTimeKind.Local).AddTicks(2660));
        }
    }
}
