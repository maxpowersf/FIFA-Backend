using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public int TournamentID { get; set; }
        public int TeamID { get; set; }
        public string Result { get; set; }
        public int NoPosition { get; set; }
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
