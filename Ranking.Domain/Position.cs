using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Position
    {
        public int Id { get; set; }
        public int TournamentID { get; set; }
        private Tournament Tournament { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public string TeamName { get { return Team != null ? Team.Name : null; } }
        public string Result { get; set; }
        public int NoPosition { get; set; }
        public int Points { get { return Wins * 3 + Draws; } }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public string Round { get; set; }
        public string Group { get; set; }
        public bool Qualified { get; set; }
    }
}
