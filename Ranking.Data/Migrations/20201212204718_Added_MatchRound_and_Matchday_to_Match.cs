using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Added_MatchRound_and_Matchday_to_Match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchRoundID",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Matchday",
                table: "Matches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchRoundID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Matchday",
                table: "Matches");
        }
    }
}
