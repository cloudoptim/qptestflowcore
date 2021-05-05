using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class AddForeignkeyforuserroletoorguserandrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 5, 21, 31, 28, 877, DateTimeKind.Local).AddTicks(9335));

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_roleid",
                table: "UserRoles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_userclientid",
                table: "UserRoles",
                column: "userclientid");

            migrationBuilder.AddForeignKey(
                name: "fk_orguser_userrole_userclientid_userid",
                table: "UserRoles",
                column: "userclientid",
                principalTable: "OrgUsers",
                principalColumn: "userid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_role_userrole_roleid_roleid",
                table: "UserRoles",
                column: "roleid",
                principalTable: "Roles",
                principalColumn: "roleid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orguser_userrole_userclientid_userid",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_role_userrole_roleid_roleid",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_roleid",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_userclientid",
                table: "UserRoles");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 5, 21, 18, 46, 119, DateTimeKind.Local).AddTicks(2000));
        }
    }
}
