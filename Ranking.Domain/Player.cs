using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public int ConfederationsGoals { get; set; }
        public int WorldCupGoals { get; set; }
        public string GoldenBoots { get; set; }
    }
}
