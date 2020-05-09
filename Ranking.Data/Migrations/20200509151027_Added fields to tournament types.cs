using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Addedfieldstotournamenttypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfederationID",
                table: "TournamentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormatID",
                table: "TournamentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoTeams",
                table: "TournamentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTypes_ConfederationID",
                table: "TournamentTypes",
                column: "ConfederationID");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentTypes_Confederations_ConfederationID",
                table: "TournamentTypes",
                column: "ConfederationID",
                principalTable: "Confederations",
                principalColumn: "ConfederationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentTypes_Confederations_ConfederationID",
                table: "TournamentTypes");

            migrationBuilder.DropIndex(
                name: "IX_TournamentTypes_ConfederationID",
                table: "TournamentTypes");

            migrationBuilder.DropColumn(
                name: "ConfederationID",
                table: "TournamentTypes");

            migrationBuilder.DropColumn(
                name: "FormatID",
                table: "TournamentTypes");

            migrationBuilder.DropColumn(
                name: "NoTeams",
                table: "TournamentTypes");
        }
    }
}
