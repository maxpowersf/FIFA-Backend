﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class MatchDTO
    {
        public DateTime Date { get; set; }
        public int MatchTypeId { get; set; }
        public int TournamentId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int GoalsTeam1 { get; set; }
        public int GoalsTeam2 { get; set; }
        public int PenaltiesTeam1 { get; set; }
        public int PenaltiesTeam2 { get; set; }
    }
}
