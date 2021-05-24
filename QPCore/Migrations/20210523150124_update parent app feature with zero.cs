using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class updateparentappfeaturewithzero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 23, 22, 1, 23, 782, DateTimeKind.Local).AddTicks(810));
            
            migrationBuilder.Sql(@"UPDATE public.""ApplicationFeatures"" SET ""ParentFeatureId"" = null WHERE ""ParentFeatureId"" = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 23, 15, 38, 1, 716, DateTimeKind.Local).AddTicks(80));
        }
    }
}
