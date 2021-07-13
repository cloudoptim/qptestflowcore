using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addfeatureworkitemtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemTypes",
                columns: new[] { "id", "created_by", "created_date", "name", "updated_by", "updated_date" },
                values: new object[] { 6, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feature", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemTypes",
                keyColumn: "id",
                keyValue: 6);
        }
    }
}
