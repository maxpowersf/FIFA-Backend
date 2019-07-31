using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Rankings
    {
        public int RankingID { get; set; }
        public int Year { get; set; }
        public int TeamID { get; set; }
        public Teams Team { get; set; }
        public decimal Points { get; set; }
    }
}
