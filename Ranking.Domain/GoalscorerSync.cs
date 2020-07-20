using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class GoalscorerSync
    {
        public GoalscorerSync(string player, string team, int goals, bool goldenBoot)
        {
            Player = player;
            Team = team;
            Goals = goals;
            GoldenBoot = goldenBoot;
        }

        public string Player { get; set; }
        public string Team { get; set; }
        public int Goals { get; set; }
        public bool GoldenBoot { get; set; }
    }
}
