using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class TournamentCurrentStandingsResponse
    {
        public Tournament Tournament { get; set; }
        public List<RoundMatches> Playoffs { get; set; }
        public List<Group> Groups { get; set; }
        public List<RoundMatches> Rounds { get; set; }
        public TournamentCurrentStandingsResponse()
        {
            this.Playoffs = new List<RoundMatches>();
            this.Groups = new List<Group>();
            this.Rounds = new List<RoundMatches>();
        }
    }
}