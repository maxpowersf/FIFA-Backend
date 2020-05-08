using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Changedtitlesdatabehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.AddColumn<int>(
                name: "ConfederationTournamentTitles",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConfederationsCupTitles",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldCupTitles",
                table: "Teams",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfederationTournamentTitles",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ConfederationsCupTitles",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WorldCupTitles",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoTitles = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    TournamentTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleID);
                    table.ForeignKey(
                        name: "FK_Titles_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Titles_TournamentTypes_TournamentTypeID",
                        column: x => x.TournamentTypeID,
                        principalTable: "TournamentTypes",
                        principalColumn: "TournamentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Titles_TeamID",
                table: "Titles",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_TournamentTypeID",
                table: "Titles",
                column: "TournamentTypeID");
        }
    }
}
