using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Positions
    {
        public int PositionID { get; set; }
        public int TournamentID { get; set; }
        public Tournaments Tournament { get; set; }
        public int TeamID { get; set; }
        public Teams Team { get; set; }
        public string Result { get; set; }
        public int NoPosition { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public bool Qualified { get; set; }
    }
}
