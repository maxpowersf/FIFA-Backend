using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Added_MatchType_FK_to_TournamentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchTypeID",
                table: "TournamentTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTypes_MatchTypeID",
                table: "TournamentTypes",
                column: "MatchTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentTypes_MatchTypes_MatchTypeID",
                table: "TournamentTypes",
                column: "MatchTypeID",
                principalTable: "MatchTypes",
                principalColumn: "MatchTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentTypes_MatchTypes_MatchTypeID",
                table: "TournamentTypes");

            migrationBuilder.DropIndex(
                name: "IX_TournamentTypes_MatchTypeID",
                table: "TournamentTypes");

            migrationBuilder.DropColumn(
                name: "MatchTypeID",
                table: "TournamentTypes");
        }
    }
}
