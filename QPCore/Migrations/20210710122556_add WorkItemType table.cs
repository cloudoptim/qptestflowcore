using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addWorkItemTypetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "work_item_type_id",
                table: "WorkItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkItemTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workitemtype_id", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "WorkItemTypes",
                columns: new[] { "id", "created_by", "created_date", "name", "updated_by", "updated_date" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Story", null, null },
                    { 2, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product backlog item", null, null },
                    { 3, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task", null, null },
                    { 4, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bug", null, null },
                    { 5, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Issue", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_work_item_type_id",
                table: "WorkItems",
                column: "work_item_type_id");

            migrationBuilder.CreateIndex(
                name: "uq_workiten_type_name",
                table: "WorkItemTypes",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_workitem_workitemtype_workitemtypeid",
                table: "WorkItems",
                column: "work_item_type_id",
                principalTable: "WorkItemTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workitem_workitemtype_workitemtypeid",
                table: "WorkItems");

            migrationBuilder.DropTable(
                name: "WorkItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_work_item_type_id",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "work_item_type_id",
                table: "WorkItems");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 7, 10, 17, 25, 54, 663, DateTimeKind.Local).AddTicks(5930));
        }
    }
}
