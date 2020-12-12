using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class Removed_MatchType_from_Match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchTypes_MatchTypeID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_MatchTypeID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchTypeID",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchTypeID",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchTypeID",
                table: "Matches",
                column: "MatchTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchTypes_MatchTypeID",
                table: "Matches",
                column: "MatchTypeID",
                principalTable: "MatchTypes",
                principalColumn: "MatchTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
