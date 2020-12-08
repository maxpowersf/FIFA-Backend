using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class TeamStat
    {
        public int Id { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public decimal Effectiveness { get; set; }
    }
}
