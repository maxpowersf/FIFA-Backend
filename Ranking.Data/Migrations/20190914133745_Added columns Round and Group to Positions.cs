using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class AddedcolumnsRoundandGrouptoPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Round",
                table: "Positions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Positions");
        }
    }
}
