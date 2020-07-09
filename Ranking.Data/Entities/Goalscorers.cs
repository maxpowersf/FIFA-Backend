using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Goalscorers
    {
        public int GoalscorerID { get; set; }
        public int TournamentID { get; set; }
        public Tournaments Tournament { get; set; }
        public int PlayerID { get; set; }
        public Players Player { get; set; }
        public int Goals { get; set; }
        public bool GoldenBoot { get; set; }
    }
}
