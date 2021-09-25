using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class RoundMatches
    {
        public string RoundName { get; set; }
        public Boolean IsHomeAway { get; set; }
        public List<Match> Matches { get; set; }

        public RoundMatches()
        {
            Matches = new List<Match>();
        }
    }
}
