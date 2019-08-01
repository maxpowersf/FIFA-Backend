using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class AddedTournamentTypesandTournamentstoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentTypes",
                columns: table => new
                {
                    TournamentTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTypes", x => x.TournamentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    NoOfTeams = table.Column<int>(nullable: false),
                    TournamentTypeID = table.Column<int>(nullable: false),
                    ConfederationID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentID);
                    table.ForeignKey(
                        name: "FK_Tournaments_Confederations_ConfederationID",
                        column: x => x.ConfederationID,
                        principalTable: "Confederations",
                        principalColumn: "ConfederationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentTypes_TournamentTypeID",
                        column: x => x.TournamentTypeID,
                        principalTable: "TournamentTypes",
                        principalColumn: "TournamentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_ConfederationID",
                table: "Tournaments",
                column: "ConfederationID");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentTypeID",
                table: "Tournaments",
                column: "TournamentTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentTypes");
        }
    }
}
