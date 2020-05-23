using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Players
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int PositionID { get; set; }
        public int TeamID { get; set; }
        public Teams Team { get; set; }
        public int Dorsal { get; set; }
        public int ConfederationsGoals { get; set; }
        public int WorldCupGoals { get; set; }
        public int ConfederationsGoldenBoots { get; set; }
        public int WorldCupGoldenBoots { get; set; }
    }
}
