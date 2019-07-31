using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Match
    {
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public decimal Team1Points { get; set; }
        public decimal Team2Points { get; set; }
    }
}
