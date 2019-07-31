using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class RemoveddatecolumnsinTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateHighestRank",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DateLowestRank",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateHighestRank",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLowestRank",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
