using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class TournamentCurrentStandingsResponse
    {
        public Tournament Tournament { get; set; }
        public List<Group> Groups { get; set; }
        public TournamentCurrentStandingsResponse()
        {
            this.Groups = new List<Group>();
        }
    }
}