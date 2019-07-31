using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class MatchTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
