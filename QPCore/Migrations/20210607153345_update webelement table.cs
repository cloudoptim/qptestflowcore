using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class updatewebelementtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "elementid",
                table: "WebElement",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "WebElement",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "WebElement",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "updated_by",
                table: "WebElement",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "WebElement",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 7, 22, 33, 44, 792, DateTimeKind.Local).AddTicks(5490));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "WebElement");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "WebElement");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "WebElement");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "WebElement");

            migrationBuilder.AlterColumn<int>(
                name: "elementid",
                table: "WebElement",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 4, 2, 48, 20, 856, DateTimeKind.Local).AddTicks(580));
        }
    }
}
