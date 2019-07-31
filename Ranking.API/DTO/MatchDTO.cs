using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class MatchDTO
    {
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public decimal Team1Points { get; set; }
        public decimal Team2Points { get; set; }
    }
}
