using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QPCore.Migrations
{
    public partial class updatefkforstepglossaryandapplicationfeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update public.""StepGlossary"" set ""FeatureId"" = null where not EXISTS (select 1 from public.""ApplicationFeatures"" where ""StepGlossary"".""FeatureId"" = ""ApplicationFeatures"".""AppFeatureId"")");
            
            migrationBuilder.DropTable(
                name: "StepGlossaryFeatureAssoc");

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 25, 7, 53, 34, 728, DateTimeKind.Local).AddTicks(2230));

            migrationBuilder.CreateIndex(
                name: "IX_StepGlossary_FeatureId",
                table: "StepGlossary",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "fk_stepglossary_applicationfeature_featureid",
                table: "StepGlossary",
                column: "FeatureId",
                principalTable: "ApplicationFeatures",
                principalColumn: "AppFeatureId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stepglossary_applicationfeature_featureid",
                table: "StepGlossary");

            migrationBuilder.DropIndex(
                name: "IX_StepGlossary_FeatureId",
                table: "StepGlossary");

            migrationBuilder.CreateTable(
                name: "StepGlossaryFeatureAssoc",
                columns: table => new
                {
                    FeatureAssocId = table.Column<int>(type: "integer", nullable: false),
                    Featureid = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<BitArray>(type: "bit(1)", nullable: true),
                    StepId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StepFeaureAssoc", x => x.FeatureAssocId);
                    table.ForeignKey(
                        name: "FeaureAssoc",
                        column: x => x.Featureid,
                        principalTable: "ApplicationFeatures",
                        principalColumn: "AppFeatureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "StepGlossaryAssoc",
                        column: x => x.StepId,
                        principalTable: "StepGlossary",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "orgid",
                keyValue: 1,
                column: "createddate",
                value: new DateTime(2021, 5, 23, 22, 13, 20, 438, DateTimeKind.Local).AddTicks(3720));

            migrationBuilder.CreateIndex(
                name: "IX_StepGlossaryFeatureAssoc_Featureid",
                table: "StepGlossaryFeatureAssoc",
                column: "Featureid");

            migrationBuilder.CreateIndex(
                name: "IX_StepGlossaryFeatureAssoc_StepId",
                table: "StepGlossaryFeatureAssoc",
                column: "StepId");
        }
    }
}
