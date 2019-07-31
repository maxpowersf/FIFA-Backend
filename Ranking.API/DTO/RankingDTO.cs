using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class RankingDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TeamID { get; set; }
        public decimal Points { get; set; }
    }
}
