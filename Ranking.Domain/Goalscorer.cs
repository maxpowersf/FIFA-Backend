using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Goalscorer
    {
        public int Id { get; set; }
        public int TournamentID { get; set; }
        private Tournament Tournament { get; set; }
        public int PlayerID { get; set; }
        public Player Player { get; set; }
        public string PlayerName { get { return Player != null ? Player.FullName : null; } }
        public int Goals { get; set; }
        public bool GoldenBoot { get; set; }
    }
}
