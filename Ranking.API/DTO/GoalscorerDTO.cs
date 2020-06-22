using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class GoalscorerDTO
    {
        public int Id { get; set; }
        public int TournamentID { get; set; }
        public int PlayerID { get; set; }
        public int Goals { get; set; }
        public bool GoldenBoot { get; set; }
    }
}
