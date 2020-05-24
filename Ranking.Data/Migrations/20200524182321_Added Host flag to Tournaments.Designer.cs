﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ranking.Data;

namespace Ranking.Data.Migrations
{
    [DbContext(typeof(RankingContext))]
    [Migration("20200524182321_Added Host flag to Tournaments")]
    partial class AddedHostflagtoTournaments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ranking.Data.Entities.Confederations", b =>
                {
                    b.Property<int>("ConfederationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.Property<decimal>("Weight");

                    b.HasKey("ConfederationID");

                    b.ToTable("Confederations");
                });

            modelBuilder.Entity("Ranking.Data.Entities.MatchTypes", b =>
                {
                    b.Property<int>("MatchTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Weight");

                    b.HasKey("MatchTypeID");

                    b.ToTable("MatchTypes");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Players", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfederationsGoals");

                    b.Property<int>("ConfederationsGoldenBoots");

                    b.Property<int>("Dorsal");

                    b.Property<string>("Name");

                    b.Property<int>("PositionID");

                    b.Property<int>("TeamID");

                    b.Property<int>("WorldCupGoals");

                    b.Property<int>("WorldCupGoldenBoots");

                    b.HasKey("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Positions", b =>
                {
                    b.Property<int>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Draws");

                    b.Property<int>("GamesPlayed");

                    b.Property<int>("GoalsAgainst");

                    b.Property<int>("GoalsFavor");

                    b.Property<string>("Group");

                    b.Property<int>("Loses");

                    b.Property<int>("NoPosition");

                    b.Property<bool>("Qualified");

                    b.Property<string>("Result");

                    b.Property<string>("Round");

                    b.Property<int>("TeamID");

                    b.Property<int>("TournamentID");

                    b.Property<int>("Wins");

                    b.HasKey("PositionID");

                    b.HasIndex("TeamID");

                    b.HasIndex("TournamentID");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Rankings", b =>
                {
                    b.Property<int>("RankingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Points");

                    b.Property<int>("TeamID");

                    b.Property<int>("Year");

                    b.HasKey("RankingID");

                    b.HasIndex("TeamID");

                    b.ToTable("Rankings");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Teams", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualRank");

                    b.Property<int>("ConfederationID");

                    b.Property<int>("ConfederationTournamentTitles");

                    b.Property<int>("ConfederationsCupTitles");

                    b.Property<string>("Flag");

                    b.Property<int>("HighestRank");

                    b.Property<int>("LowestRank");

                    b.Property<string>("Name");

                    b.Property<int>("RankingChange");

                    b.Property<decimal>("TotalPoints");

                    b.Property<int>("WorldCupQualifications");

                    b.Property<int>("WorldCupTitles");

                    b.HasKey("TeamID");

                    b.HasIndex("ConfederationID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Ranking.Data.Entities.TournamentTypes", b =>
                {
                    b.Property<int>("TournamentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConfederationID");

                    b.Property<int>("FormatID");

                    b.Property<string>("Name");

                    b.Property<int>("NoTeams");

                    b.HasKey("TournamentTypeID");

                    b.HasIndex("ConfederationID");

                    b.ToTable("TournamentTypes");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Tournaments", b =>
                {
                    b.Property<int>("TournamentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConfederationID");

                    b.Property<string>("Host");

                    b.Property<string>("HostFlag");

                    b.Property<string>("Name");

                    b.Property<int>("NoOfTeams");

                    b.Property<int>("TournamentTypeID");

                    b.Property<int>("Year");

                    b.HasKey("TournamentID");

                    b.HasIndex("ConfederationID");

                    b.HasIndex("TournamentTypeID");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Players", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ranking.Data.Entities.Positions", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Teams", "Team")
                        .WithMany("Positions")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ranking.Data.Entities.Tournaments", "Tournament")
                        .WithMany("Positions")
                        .HasForeignKey("TournamentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ranking.Data.Entities.Rankings", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Teams", "Team")
                        .WithMany("Rankings")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ranking.Data.Entities.Teams", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Confederations", "Confederation")
                        .WithMany("Teams")
                        .HasForeignKey("ConfederationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ranking.Data.Entities.TournamentTypes", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Confederations", "Confederation")
                        .WithMany()
                        .HasForeignKey("ConfederationID");
                });

            modelBuilder.Entity("Ranking.Data.Entities.Tournaments", b =>
                {
                    b.HasOne("Ranking.Data.Entities.Confederations", "Confederation")
                        .WithMany()
                        .HasForeignKey("ConfederationID");

                    b.HasOne("Ranking.Data.Entities.TournamentTypes", "TournamentType")
                        .WithMany()
                        .HasForeignKey("TournamentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
