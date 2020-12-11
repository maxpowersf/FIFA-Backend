using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class H2H
    {
        public int H2HID { get; set; }
        public int Team1ID { get; set; }
        public Teams Team1 { get; set; }
        public int Team2ID { get; set; }
        public Teams Team2 { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}
