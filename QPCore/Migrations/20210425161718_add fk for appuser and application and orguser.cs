using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addfkforappuserandapplicationandorguser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 23, 17, 17, 226, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_client",
                table: "AppUsers",
                column: "client");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_userid",
                table: "AppUsers",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "fk_application_appuser_client",
                table: "AppUsers",
                column: "client",
                principalTable: "Application",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_orguser_appuser_userid",
                table: "AppUsers",
                column: "userid",
                principalTable: "OrgUsers",
                principalColumn: "userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_application_appuser_client",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_orguser_appuser_userid",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_client",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_userid",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 4, 25, 22, 21, 27, 493, DateTimeKind.Local).AddTicks(1630));
        }
    }
}
