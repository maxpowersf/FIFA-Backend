﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConfederationID { get; set; }
        public int ActualRank { get; set; }
        public int LowestRank { get; set; }
        public int HighestRank { get; set; }
        public decimal TotalPoints { get; set; }
    }
}
