using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ranking.Data.Entities
{
    public class Matches
    {
        public int MatchID { get; set; }
        public DateTime Date { get; set; }
        public int TournamentID { get; set; }
        public Tournaments Tournament { get; set; }
        public int Team1ID { get; set; }
        public Teams Team1 { get; set; }
        public int Team2ID { get; set; }
        public Teams Team2 { get; set; }
        public int GoalsTeam1 { get; set; }
        public int PenaltiesTeam1 { get; set; }
        public int GoalsTeam2 { get; set; }
        public int PenaltiesTeam2 { get; set; }
        public int MatchResultID { get; set; }
    }
}
