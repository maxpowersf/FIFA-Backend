using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Added_TeamStats_and_H2H : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H2H",
                columns: table => new
                {
                    H2HID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Team1ID = table.Column<int>(nullable: false),
                    Team2ID = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false),
                    Loses = table.Column<int>(nullable: false),
                    GoalsFavor = table.Column<int>(nullable: false),
                    GoalsAgainst = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2H", x => x.H2HID);
                    table.ForeignKey(
                        name: "FK_H2H_Teams_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_H2H_Teams_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "H2HWorldCup",
                columns: table => new
                {
                    H2HWorldCupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Team1ID = table.Column<int>(nullable: false),
                    Team2ID = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false),
                    Loses = table.Column<int>(nullable: false),
                    GoalsFavor = table.Column<int>(nullable: false),
                    GoalsAgainst = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2HWorldCup", x => x.H2HWorldCupID);
                    table.ForeignKey(
                        name: "FK_H2HWorldCup_Teams_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_H2HWorldCup_Teams_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    TeamStatsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamID = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false),
                    Loses = table.Column<int>(nullable: false),
                    GoalsFavor = table.Column<int>(nullable: false),
                    GoalsAgainst = table.Column<int>(nullable: false),
                    GoalDifference = table.Column<int>(nullable: false),
                    Effectiveness = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => x.TeamStatsID);
                    table.ForeignKey(
                        name: "FK_TeamStats_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatsWorldCup",
                columns: table => new
                {
                    TeamStatsWorldCupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamID = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false),
                    Loses = table.Column<int>(nullable: false),
                    GoalsFavor = table.Column<int>(nullable: false),
                    GoalsAgainst = table.Column<int>(nullable: false),
                    GoalDifference = table.Column<int>(nullable: false),
                    Effectiveness = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatsWorldCup", x => x.TeamStatsWorldCupID);
                    table.ForeignKey(
                        name: "FK_TeamStatsWorldCup_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_H2H_Team1ID",
                table: "H2H",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_H2H_Team2ID",
                table: "H2H",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_H2HWorldCup_Team1ID",
                table: "H2HWorldCup",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_H2HWorldCup_Team2ID",
                table: "H2HWorldCup",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_TeamID",
                table: "TeamStats",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatsWorldCup_TeamID",
                table: "TeamStatsWorldCup",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H2H");

            migrationBuilder.DropTable(
                name: "H2HWorldCup");

            migrationBuilder.DropTable(
                name: "TeamStats");

            migrationBuilder.DropTable(
                name: "TeamStatsWorldCup");
        }
    }
}
