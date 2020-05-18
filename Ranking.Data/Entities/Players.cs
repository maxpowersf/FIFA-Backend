using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Players
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int TeamID { get; set; }
        public Teams Team { get; set; }
        public int ConfederationsGoals { get; set; }
        public int WorldCupGoals { get; set; }
        public string GoldenBoots { get; set; }
    }
}
