using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class RevertConfederationentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Confederations_ConfederationsConfederationID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ConfederationsConfederationID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ConfederationsConfederationID",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfederationsConfederationID",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ConfederationsConfederationID",
                table: "Teams",
                column: "ConfederationsConfederationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Confederations_ConfederationsConfederationID",
                table: "Teams",
                column: "ConfederationsConfederationID",
                principalTable: "Confederations",
                principalColumn: "ConfederationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
