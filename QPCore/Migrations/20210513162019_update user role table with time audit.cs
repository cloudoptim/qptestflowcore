using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class updateuserroletablewithtimeaudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "UserRoles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "UserRoles",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "userclientid",
                table: "UserRoles",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_userclientid",
                table: "UserRoles",
                newName: "IX_UserRoles_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_roleid",
                table: "UserRoles",
                newName: "IX_UserRoles_role_id");

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "UserRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "UserRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "UserRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "UserRoles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 13, 23, 20, 18, 160, DateTimeKind.Local).AddTicks(3249));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "UserRoles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "UserRoles",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserRoles",
                newName: "userclientid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_user_id",
                table: "UserRoles",
                newName: "IX_UserRoles_userclientid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_role_id",
                table: "UserRoles",
                newName: "IX_UserRoles_roleid");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 13, 22, 26, 9, 768, DateTimeKind.Local).AddTicks(4375));
        }
    }
}
