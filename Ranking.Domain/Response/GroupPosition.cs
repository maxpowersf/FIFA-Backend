using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class GroupPosition
    {
        public Team Team { get; set; }
        public int NoPosition { get; set; }
        public int Points { get { return (Wins * 3) + Draws; } }
        public int GamesPlayed { get { return Wins + Draws + Loses; } }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get { return GoalsFavor - GoalsAgainst; } }

        public GroupPosition(Team team)
        {
            this.Team = team;
            this.NoPosition = 1;
            this.Wins = 0;
            this.Draws = 0;
            this.Loses = 0;
            this.GoalsFavor = 0;
            this.GoalsAgainst = 0;
        }
    }
}
