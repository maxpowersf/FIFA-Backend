using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public PlayerPosition Position { get; set; }
        public string PositionName { get { return Position.ToString(); } }
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public int Dorsal { get; set; }
        public int TotalGoals { get { return QualificationGoals + ConfederationTournamentGoals + ConfederationsGoals + WorldCupGoals; } }
        public int QualificationGoals { get; set; }
        public int ConfederationTournamentGoals { get; set; }
        public int ConfederationsGoals { get; set; }
        public int WorldCupGoals { get; set; }
        public int ConfederationsGoldenBoots { get; set; }
        public int WorldCupGoldenBoots { get; set; }
    }
}
