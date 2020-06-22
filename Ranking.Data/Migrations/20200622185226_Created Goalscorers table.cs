using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class CreatedGoalscorerstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goalscorers",
                columns: table => new
                {
                    GoalscorerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TournamentID = table.Column<int>(nullable: false),
                    PlayerID = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    GoldenBoot = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goalscorers", x => x.GoalscorerID);
                    table.ForeignKey(
                        name: "FK_Goalscorers_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goalscorers_Tournaments_TournamentID",
                        column: x => x.TournamentID,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goalscorers_PlayerID",
                table: "Goalscorers",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Goalscorers_TournamentID",
                table: "Goalscorers",
                column: "TournamentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goalscorers");
        }
    }
}
