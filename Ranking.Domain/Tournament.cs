using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Host { get; set; }
        public int NoOfTeams { get; set; }
        public TournamentType TournamentType { get; set; }
        public string TournamentTypeName { get { return TournamentType != null ? TournamentType.Name : null; } }
        public Confederation Confederation { get; set; }
        public string ConfederationName { get { return Confederation != null ? Confederation.Name : null; } }
    }
}
