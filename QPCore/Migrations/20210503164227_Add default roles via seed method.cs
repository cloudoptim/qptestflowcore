using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class Adddefaultrolesviaseedmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 3, 23, 42, 26, 639, DateTimeKind.Local).AddTicks(6453));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "roleid", "isactive", "isdefault", "issystem", "rolename" },
                values: new object[,]
                {
                    { 2, true, true, true, "User" },
                    { 1, true, false, true, "Administrator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "roleid",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "roleid",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createdDate",
                value: new DateTime(2021, 5, 3, 23, 29, 5, 655, DateTimeKind.Local).AddTicks(400));
        }
    }
}
