using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class AddedcolumnstoPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfederationTournamentGoals",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QualificationGoals",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfederationTournamentGoals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "QualificationGoals",
                table: "Players");
        }
    }
}
