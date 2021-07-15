using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QPCore.Migrations
{
    public partial class addintegrationstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntegrationSources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    logo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    readme = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_integrationsource_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    source_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    pat = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    organization = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    project = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_intergration_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_integrationsource_integration_source_id",
                        column: x => x.source_id,
                        principalTable: "IntegrationSources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orguser_integration_user_id",
                        column: x => x.user_id,
                        principalTable: "OrgUsers",
                        principalColumn: "user-id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IntegrationSources",
                columns: new[] { "id", "created_by", "created_date", "logo", "name", "readme", "updated_by", "updated_date" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "azure.png", "Azure", "Readme", null, null },
                    { 2, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jira.png", "Jira", "Readme", null, null },
                    { 3, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "agm.jpeg", "AGM", "Readme", null, null },
                    { 4, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "slack.png", "Slack", "Readme", null, null },
                    { 5, 1, new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jenkins.png", "Jenkins", "Readme", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_source_id",
                table: "Integrations",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_user_id",
                table: "Integrations",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropTable(
                name: "IntegrationSources");
        }
    }
}
