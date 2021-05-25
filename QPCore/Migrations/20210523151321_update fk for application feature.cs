using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class updatefkforapplicationfeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 23, 22, 13, 20, 438, DateTimeKind.Local).AddTicks(3720));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeatures_ParentFeatureId",
                table: "ApplicationFeatures",
                column: "ParentFeatureId");

            migrationBuilder.AddForeignKey(
                name: "fk_self_parent_id",
                table: "ApplicationFeatures",
                column: "ParentFeatureId",
                principalTable: "ApplicationFeatures",
                principalColumn: "AppFeatureId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_self_parent_id",
                table: "ApplicationFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationFeatures_ParentFeatureId",
                table: "ApplicationFeatures");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 23, 22, 1, 23, 782, DateTimeKind.Local).AddTicks(810));
        }
    }
}
