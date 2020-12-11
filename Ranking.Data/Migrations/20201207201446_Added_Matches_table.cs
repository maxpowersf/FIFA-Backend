using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Added_Matches_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchTypeID = table.Column<int>(nullable: false),
                    TournamentID = table.Column<int>(nullable: false),
                    Team1ID = table.Column<int>(nullable: false),
                    Team2ID = table.Column<int>(nullable: false),
                    GoalsTeam1 = table.Column<int>(nullable: false),
                    PenaltiesTeam1 = table.Column<int>(nullable: false),
                    GoalsTeam2 = table.Column<int>(nullable: false),
                    PenaltiesTeam2 = table.Column<int>(nullable: false),
                    MatchResultID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchID);
                    table.ForeignKey(
                        name: "FK_Matches_MatchTypes_MatchTypeID",
                        column: x => x.MatchTypeID,
                        principalTable: "MatchTypes",
                        principalColumn: "MatchTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentID",
                        column: x => x.TournamentID,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchTypeID",
                table: "Matches",
                column: "MatchTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1ID",
                table: "Matches",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2ID",
                table: "Matches",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentID",
                table: "Matches",
                column: "TournamentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
