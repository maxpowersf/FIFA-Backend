using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class ChangedGoldenBootsinPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoldenBoots",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "ConfederationsGoldenBoots",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldCupGoldenBoots",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfederationsGoldenBoots",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WorldCupGoldenBoots",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "GoldenBoots",
                table: "Players",
                nullable: true);
        }
    }
}
