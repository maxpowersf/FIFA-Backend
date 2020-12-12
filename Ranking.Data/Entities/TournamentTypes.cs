using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class TournamentTypes
    {
        public int TournamentTypeID { get; set; }
        public string Name { get; set; }
        public int? MatchTypeID { get; set; }
        public MatchTypes MatchType { get; set; }
        public int FormatID { get; set; }
        public int? ConfederationID { get; set; }
        public Confederations Confederation { get; set; }
        public int NoTeams { get; set; }
    }
}
