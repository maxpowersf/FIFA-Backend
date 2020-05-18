using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Teams
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public int ConfederationID { get; set; }
        public Confederations Confederation { get; set; }
        public int ActualRank { get; set; }
        public int LowestRank { get; set; }
        public int HighestRank { get; set; }
        public int RankingChange { get; set; }
        public decimal TotalPoints { get; set; }
        public virtual ICollection<Rankings> Rankings { get; set; }
        public ICollection<Positions> Positions { get; set; }
        public int WorldCupTitles { get; set; }
        public int ConfederationsCupTitles { get; set; }
        public int ConfederationTournamentTitles { get; set; }
        public int WorldCupQualifications { get; set; }
    }
}
