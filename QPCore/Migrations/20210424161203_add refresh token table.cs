using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addrefreshtokentable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "refreshtokenseq",
                minValue: 0L);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    refreshtokenid = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    orguserid = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    expires = table.Column<DateTime>(type: "date", nullable: false),
                    created = table.Column<DateTime>(type: "date", nullable: false),
                    createdbyip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    revoked = table.Column<DateTime>(type: "date", nullable: true),
                    revokedbyip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    replacedbytoken = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pr_refreshtoken_refreshtokenid", x => x.refreshtokenid);
                    table.ForeignKey(
                        name: "fk_orgusers_refreshtoken_orguserid",
                        column: x => x.orguserid,
                        principalTable: "OrgUsers",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_orguserid",
                table: "RefreshToken",
                column: "orguserid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropSequence(
                name: "refreshtokenseq");
        }
    }
}
