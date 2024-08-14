using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class TournamentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchTypeID { get; set; }
        public MatchType MatchType { get; set; }
        public string MatchTypeName { get { return MatchType != null ? MatchType.Name : null; } }
        public decimal? MatchTypeWeight { get { return MatchType?.Weight; } }
        public TournamentFormat Format { get; set; }
        public string FormatName { get { return Format.ToString(); } }
        public int? ConfederationID { get; set; }
        public Confederation Confederation { get; set; }
        public string ConfederationName { get { return Confederation != null ? Confederation.Name : null; } }
        public int NoTeams { get; set; }
    }
}
