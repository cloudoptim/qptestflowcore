using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class addemailcolumntoorguserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "OrgUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "OrgUsers");
        }
    }
}
