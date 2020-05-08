using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public int ConfederationID { get; set; }
        public Confederation Confederation { get; set; }
        public string ConfederationName { get { return Confederation != null ? Confederation.Name : null; } }
        public string ConfederationColor { get { return Confederation != null ? Confederation.Color : null; } }
        public decimal? ConfederationWeight { get { return Confederation != null ? Confederation.Weight : (decimal?)null; } }
        public int ActualRank { get; set; }
        public int LowestRank { get; set; }
        public int HighestRank { get; set; }
        public decimal TotalPoints { get; set; }
        public List<Ranking> Rankings { get; set; }
        public Ranking Ranking1 { get { return Rankings.Count > 2 ? Rankings[Rankings.Count - 3] : null; } }
        public Ranking Ranking2 { get { return Rankings.Count > 1 ? Rankings[Rankings.Count - 2] : null; } }
        public Ranking Ranking3 { get { return Rankings.Count > 0 ? Rankings[Rankings.Count - 1] : null; } }
        public int WorldCupTitles { get; set; }
        public int ConfederationsCupTitles { get; set; }
        public int ConfederationTournamentTitles { get; set; }
    }
}
