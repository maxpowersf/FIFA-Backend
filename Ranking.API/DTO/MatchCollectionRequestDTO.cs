using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class MatchCollectionRequestDTO
    {
        public int? Team1ID { get; set; }
        public int? Team2ID { get; set; }
        public int? ConfederationID { get; set; }
        public int? TournamentID { get; set; }
        public int? TournamentTypeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Quantity { get; set; }
    }
}
