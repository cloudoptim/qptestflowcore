using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class Updateorgusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enabled",
                table: "OrgUsers");

            migrationBuilder.RenameColumn(
                name: "verificationtoken",
                table: "OrgUsers",
                newName: "verification-token");

            migrationBuilder.RenameColumn(
                name: "usewindowsauth",
                table: "OrgUsers",
                newName: "use-windows-auth");

            migrationBuilder.RenameColumn(
                name: "resettokenexpires",
                table: "OrgUsers",
                newName: "reset-token-expires");

            migrationBuilder.RenameColumn(
                name: "resettoken",
                table: "OrgUsers",
                newName: "reset-token");

            migrationBuilder.RenameColumn(
                name: "passwordreset",
                table: "OrgUsers",
                newName: "password-reset");

            migrationBuilder.RenameColumn(
                name: "orgid",
                table: "OrgUsers",
                newName: "org-id");

            migrationBuilder.RenameColumn(
                name: "loginname",
                table: "OrgUsers",
                newName: "login-name");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "OrgUsers",
                newName: "last-name");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "OrgUsers",
                newName: "first-name");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "OrgUsers",
                newName: "user-id");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "OrgUsers",
                newName: "updated-date");

            migrationBuilder.AddColumn<int>(
                name: "created-by",
                table: "OrgUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created-date",
                table: "OrgUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is-active",
                table: "OrgUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "updated-by",
                table: "OrgUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 14, 23, 24, 25, 780, DateTimeKind.Local).AddTicks(4630));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created-by",
                table: "OrgUsers");

            migrationBuilder.DropColumn(
                name: "created-date",
                table: "OrgUsers");

            migrationBuilder.DropColumn(
                name: "is-active",
                table: "OrgUsers");

            migrationBuilder.DropColumn(
                name: "updated-by",
                table: "OrgUsers");

            migrationBuilder.RenameColumn(
                name: "verification-token",
                table: "OrgUsers",
                newName: "verificationtoken");

            migrationBuilder.RenameColumn(
                name: "use-windows-auth",
                table: "OrgUsers",
                newName: "usewindowsauth");

            migrationBuilder.RenameColumn(
                name: "reset-token-expires",
                table: "OrgUsers",
                newName: "resettokenexpires");

            migrationBuilder.RenameColumn(
                name: "reset-token",
                table: "OrgUsers",
                newName: "resettoken");

            migrationBuilder.RenameColumn(
                name: "password-reset",
                table: "OrgUsers",
                newName: "passwordreset");

            migrationBuilder.RenameColumn(
                name: "org-id",
                table: "OrgUsers",
                newName: "orgid");

            migrationBuilder.RenameColumn(
                name: "login-name",
                table: "OrgUsers",
                newName: "loginname");

            migrationBuilder.RenameColumn(
                name: "last-name",
                table: "OrgUsers",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "first-name",
                table: "OrgUsers",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "user-id",
                table: "OrgUsers",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "updated-date",
                table: "OrgUsers",
                newName: "created");

            migrationBuilder.RenameIndex(
                name: "IX_OrgUsers_org-id",
                table: "OrgUsers",
                newName: "IX_OrgUsers_orgid");

            migrationBuilder.AddColumn<BitArray>(
                name: "enabled",
                table: "OrgUsers",
                type: "bit(1)",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 13, 23, 20, 18, 160, DateTimeKind.Local).AddTicks(3249));
        }
    }
}
