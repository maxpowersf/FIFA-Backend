using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Tournaments
    {
        public int TournamentID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Host { get; set; }
        public int NoOfTeams { get; set; }
        public int TournamentTypeID { get; set; }
        public TournamentTypes TournamentType { get; set; }
        public int? ConfederationID { get; set; }
        public Confederations Confederation { get; set; }
        public ICollection<Positions> Positions { get; set; }
    }
}
