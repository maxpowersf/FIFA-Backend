using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Added_Points_to_TeamStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "TeamStatsWorldCup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "TeamStats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "TeamStatsWorldCup");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "TeamStats");
        }
    }
}
