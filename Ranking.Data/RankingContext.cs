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

            builder.Entity<Entities.Rankings>(entity =>
            {
                entity.HasKey(e => e.RankingID);

                entity.HasOne(e => e.Team)
                    .WithMany(e => e.Rankings)
                    .HasForeignKey(e => e.TeamID);
            });
        }

        public virtual DbSet<Entities.MatchTypes> MatchTypes { get; set; }
        public virtual DbSet<Entities.Confederations> Confederations { get; set; }
        public virtual DbSet<Entities.Teams> Teams { get; set; }
        public virtual DbSet<Entities.Rankings> Rankings { get; set; }
    }
}
