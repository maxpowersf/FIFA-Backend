using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Host { get; set; }
        public int NoOfTeams { get; set; }
        public int TournamentTypeID { get; set; }
        public int? ConfederationID { get; set; }
        public List<PositionDTO> Positions { get; set; }
    }
}
