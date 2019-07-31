using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ranking.Data.Migrations
{
    public partial class AddedtableTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ConfederationID = table.Column<int>(nullable: false),
                    ActualRank = table.Column<int>(nullable: false),
                    LowestRank = table.Column<int>(nullable: false),
                    DateLowestRank = table.Column<DateTime>(nullable: false),
                    HighestRank = table.Column<int>(nullable: false),
                    DateHighestRank = table.Column<DateTime>(nullable: false),
                    TotalPoints = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_Confederations_ConfederationID",
                        column: x => x.ConfederationID,
                        principalTable: "Confederations",
                        principalColumn: "ConfederationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ConfederationID",
                table: "Teams",
                column: "ConfederationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
