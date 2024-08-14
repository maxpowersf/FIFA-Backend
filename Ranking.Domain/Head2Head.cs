using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Head2Head
    {
        public int Id { get; set; }
        public int Team1ID { get; set; }
        public Team Team1 { get; set; }
        public int Team2ID { get; set; }
        public Team Team2 { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get { return GoalsFavor - GoalsAgainst; } }
    }
}
