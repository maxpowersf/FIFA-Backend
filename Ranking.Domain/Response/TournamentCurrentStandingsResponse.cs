using System;
using System.Collections.Generic;

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

    public class Group
    {
        public string GroupName { get; set; }
        public List<GroupPosition> Positions { get; set; }

        public Group()
        {
            Positions = new List<GroupPosition>();
        }
    }

    public class GroupPosition
    {
        public Team Team { get; set; }
        public int NoPosition { get; set; }
        public int Points { get { return (Wins * 3) + Draws; } }
        public int GamesPlayed { get { return Wins + Draws + Loses; } }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFavor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get { return GoalsFavor - GoalsAgainst; } }
        public bool Qualified { get; set; }
        public bool AsHosts { get; set; }

        public GroupPosition(Team team)
        {
            this.Team = team;
            this.NoPosition = 1;
            this.Wins = 0;
            this.Draws = 0;
            this.Loses = 0;
            this.GoalsFavor = 0;
            this.GoalsAgainst = 0;
            this.Qualified = false;
        }
    }
}