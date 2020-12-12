using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class TournamentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchTypeID { get; set; }
        public int Format { get; set; }
        public int? ConfederationID { get; set; }
        public int NoTeams { get; set; }
    }
}
