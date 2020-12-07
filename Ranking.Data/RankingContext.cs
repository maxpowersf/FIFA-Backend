using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data
{
    public class RankingContext : DbContext
    {
        public RankingContext(DbContextOptions<RankingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entities.MatchTypes>(entity =>
            {
                entity.HasKey(e => e.MatchTypeID);
            });

            builder.Entity<Entities.Matches>(entity =>
            {
                entity.HasKey(e => e.MatchID);

                entity.HasOne(e => e.MatchType)
                    .WithMany()
                    .HasForeignKey(e => e.MatchTypeID);

                entity.HasOne(e => e.Tournament)
                    .WithMany()
                    .HasForeignKey(e => e.TournamentID);

                entity.HasOne(e => e.Team1)
                    .WithMany()
                    .HasForeignKey(e => e.Team1ID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Team2)
                    .WithMany()
                    .HasForeignKey(e => e.Team2ID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Entities.Confederations>(entity =>
            {
                entity.HasKey(e => e.ConfederationID);
            });

            builder.Entity<Entities.Teams>(entity =>
            {
                entity.HasKey(e => e.TeamID);

                entity.HasOne(e => e.Confederation)
                    .WithMany(f => f.Teams)
                    .HasForeignKey(e => e.ConfederationID);
            });

            builder.Entity<Entities.Players>(entity =>
            {
                entity.HasKey(e => e.PlayerID);

                entity.HasOne(e => e.Team)
                    .WithMany()
                    .HasForeignKey(e => e.TeamID);
            });

            builder.Entity<Entities.Rankings>(entity =>
            {
                entity.HasKey(e => e.RankingID);

                entity.HasOne(e => e.Team)
                    .WithMany(e => e.Rankings)
                    .HasForeignKey(e => e.TeamID);
            });

            builder.Entity<Entities.TournamentTypes>(entity =>
            {
                entity.HasKey(e => e.TournamentTypeID);

                entity.HasOne(e => e.Confederation)
                    .WithMany()
                    .HasForeignKey(e => e.ConfederationID);
            });

            builder.Entity<Entities.Tournaments>(entity =>
            {
                entity.HasKey(e => e.TournamentID);

                entity.HasOne(e => e.TournamentType)
                    .WithMany()
                    .HasForeignKey(e => e.TournamentTypeID);

                entity.HasOne(e => e.Confederation)
                    .WithMany()
                    .HasForeignKey(e => e.ConfederationID);
            });

            builder.Entity<Entities.Positions>(entity =>
            {
                entity.HasKey(e => e.PositionID);

                entity.HasOne(e => e.Tournament)
                    .WithMany(f => f.Positions)
                    .HasForeignKey(e => e.TournamentID);

                entity.HasOne(e => e.Team)
                    .WithMany(f => f.Positions)
                    .HasForeignKey(e => e.TeamID);
            });

            builder.Entity<Entities.Goalscorers>(entity =>
            {
                entity.HasKey(e => e.GoalscorerID);

                entity.HasOne(e => e.Tournament)
                    .WithMany()
                    .HasForeignKey(e => e.TournamentID);

                entity.HasOne(e => e.Player)
                    .WithMany()
                    .HasForeignKey(e => e.PlayerID);
            });

            builder.Entity<Entities.TeamStats>(entity =>
            {
                entity.HasKey(e => e.TeamStatsID);

                entity.HasOne(e => e.Team)
                    .WithMany()
                    .HasForeignKey(e => e.TeamID);
            });

            builder.Entity<Entities.TeamStatsWorldCup>(entity =>
            {
                entity.HasKey(e => e.TeamStatsWorldCupID);

                entity.HasOne(e => e.Team)
                    .WithMany()
                    .HasForeignKey(e => e.TeamID);
            });

            builder.Entity<Entities.H2H>(entity =>
            {
                entity.HasKey(e => e.H2HID);

                entity.HasOne(e => e.Team1)
                    .WithMany()
                    .HasForeignKey(e => e.Team1ID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Team2)
                    .WithMany()
                    .HasForeignKey(e => e.Team2ID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Entities.H2HWorldCup>(entity =>
            {
                entity.HasKey(e => e.H2HWorldCupID);

                entity.HasOne(e => e.Team1)
                    .WithMany()
                    .HasForeignKey(e => e.Team1ID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Team2)
                    .WithMany()
                    .HasForeignKey(e => e.Team2ID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public virtual DbSet<Entities.MatchTypes> MatchTypes { get; set; }
        public virtual DbSet<Entities.Matches> Matches { get; set; }
        public virtual DbSet<Entities.Confederations> Confederations { get; set; }
        public virtual DbSet<Entities.Teams> Teams { get; set; }
        public virtual DbSet<Entities.Players> Players { get; set; }
        public virtual DbSet<Entities.Rankings> Rankings { get; set; }
        public virtual DbSet<Entities.TournamentTypes> TournamentTypes { get; set; }
        public virtual DbSet<Entities.Tournaments> Tournaments { get; set; }
        public virtual DbSet<Entities.Positions> Positions { get; set; }
        public virtual DbSet<Entities.Goalscorers> Goalscorers { get; set; }
        public virtual DbSet<Entities.TeamStats> TeamStats { get; set; }
        public virtual DbSet<Entities.TeamStatsWorldCup> TeamStatsWorldCup { get; set; }
        public virtual DbSet<Entities.H2H> H2H { get; set; }
        public virtual DbSet<Entities.H2HWorldCup> H2HWorldCup { get; set; }
    }
}
