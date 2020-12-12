using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
        public int Team1ID { get; set; }
        public Team Team1 { get; set; }
        public int Team2ID { get; set; }
        public Team Team2 { get; set; }
        public int GoalsTeam1 { get; set; }
        public int PenaltiesTeam1 { get; set; }
        public int GoalsTeam2 { get; set; }
        public int PenaltiesTeam2 { get; set; }
        public MatchResult MatchResult { get; set; }
        public string Result { get { return MatchResult.ToString(); } }
    }
}
