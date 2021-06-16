using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class StreakCollectionResponse
    {
        public Team Team { get; set; }
        public string TeamName { get { return Team != null ? Team.Name : null; } }
        public string ConfederationName { get { return Team != null ? Team.Confederation.Name : null; } }
        public int Streak { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public List<Match> Matches { get; set; }
    }
}
