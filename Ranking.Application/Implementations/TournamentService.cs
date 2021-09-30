using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using Ranking.Domain.Enum;
using Ranking.Domain.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPositionRepository _positionRepository;

        public TournamentService(
            ITournamentRepository tournamentRepository,
            ITeamRepository teamRepository,
            IMatchRepository matchRepository,
            IPositionRepository positionRepository)
        {
            this._tournamentRepository = tournamentRepository;
            this._teamRepository = teamRepository;
            this._matchRepository = matchRepository;
            this._positionRepository = positionRepository;
        }

        public Task<List<Tournament>> Get()
        {
            return _tournamentRepository.Get();
        }

        public async Task<List<Tournament>> GetByTeam(int teamId)
        {
            Team team = await _teamRepository.Get(teamId);
            List<Tournament> tournamentList = await _tournamentRepository.GetByTeam(team.Id, team.ConfederationID);
            return tournamentList;
        }

        public async Task<List<Tournament>> GetByTournamentTypeWithPositions(int tournamentTypeId)
        {
            List<Tournament> tournamentList = await _tournamentRepository.GetByTournamentTypeWithPositions(tournamentTypeId);
            return tournamentList;
        }

        public async Task<List<Tournament>> GetByTournamentTypeAndConfederation(int tournamentTypeId, int confederationId)
        {
            List<Tournament> tournamentList = await _tournamentRepository.GetByTournamentTypeAndConfederation(tournamentTypeId, confederationId);
            return tournamentList;
        }

        public async Task<TournamentCurrentStandingsResponse> GetCurrentStandings(int id)
        {
            var response = new TournamentCurrentStandingsResponse();

            var tournament = await _tournamentRepository.GetWithoutPositions(id);
            if (tournament != null)
            {
                response.Tournament = tournament;

                var matches = await _matchRepository.GetByTournament(id);
                if(matches.Count <= 0)
                {
                    return null;
                }

                var groups = matches.Where(e => e.Group != "")
                                    .GroupBy(e => e.Group)
                                    .Select(e => e.ToList())
                                    .ToList();

                foreach(List<Match> group in groups)
                {
                    Group newGroup = new Group();
                    foreach(Match match in group)
                    {
                        if(newGroup.GroupName == null)
                        {
                            newGroup.GroupName = match.Group;
                        }

                        GroupPosition positionTeam1 = newGroup.Positions.FirstOrDefault(e => e.Team.Id == match.Team1ID);
                        if(positionTeam1 == null)
                        {
                            positionTeam1 = new GroupPosition(match.Team1);
                            newGroup.Positions.Add(positionTeam1);
                        }

                        GroupPosition positionTeam2 = newGroup.Positions.FirstOrDefault(e => e.Team.Id == match.Team2ID);
                        if (positionTeam2 == null)
                        {
                            positionTeam2 = new GroupPosition(match.Team2);
                            newGroup.Positions.Add(positionTeam2);
                        }

                        switch (match.MatchResult)
                        {
                            case MatchResult.Home:
                                positionTeam1.Wins++;
                                positionTeam2.Loses++;
                                break;
                            case MatchResult.Away:
                                positionTeam1.Loses++;
                                positionTeam2.Wins++;
                                break;
                            case MatchResult.Draw:
                                positionTeam1.Draws++;
                                positionTeam2.Draws++;
                                break;
                        }

                        positionTeam1.GoalsFavor += match.GoalsTeam1;
                        positionTeam1.GoalsAgainst += match.GoalsTeam2;
                        positionTeam2.GoalsFavor += match.GoalsTeam2;
                        positionTeam2.GoalsAgainst += match.GoalsTeam1;
                    }

                    newGroup.Positions = newGroup.Positions.OrderByDescending(e => e.Points)
                                                            .ThenByDescending(e => e.GoalDifference)
                                                            .ThenByDescending(e => e.GoalsFavor)
                                                            .ThenBy(e => e.Team.Name)
                                                            .ToList();

                    newGroup.Positions.ForEach(e => e.NoPosition = newGroup.Positions.IndexOf(e) + 1);

                    response.Groups.Add(newGroup);
                }

                var playoffs = matches.Where(e => e.MatchRound == MatchRound.Playoff)
                                        .GroupBy(e => e.MatchRound)
                                        .Select(e => new RoundMatches
                                        {
                                            RoundName = GetMatchRoundDescription(e.FirstOrDefault().MatchRound),
                                            Matches = e.ToList()
                                        })
                                        .ToList();
                response.Playoffs.AddRange(playoffs);

                var rounds = matches.Where(e => e.MatchRound != MatchRound.Group && e.MatchRound != MatchRound.Playoff)
                                    .GroupBy(e => e.MatchRound)
                                    .Select(e => new RoundMatches
                                    {
                                        RoundName = GetMatchRoundDescription(e.FirstOrDefault().MatchRound),
                                        Matches = e.ToList()
                                    })
                                    .ToList();

                if(tournament.TournamentType.Format == TournamentFormat.Qualification)
                {
                    if (rounds.Count == 1)
                    {
                        rounds.FirstOrDefault().RoundName = "Play Offs";
                        rounds.FirstOrDefault().IsHomeAway = IsHomeAndAway(rounds.FirstOrDefault());
                    }

                    if (tournament.FinalPositions) {
                        var positions = await _positionRepository.GetByTournament(tournament.Id);
                        var tournamentToQualify = await _tournamentRepository.GetByQualificationYear(tournament.Year);

                        foreach(Group group in response.Groups)
                        {
                            foreach(GroupPosition position in group.Positions)
                            {
                                var tournamentPosition = positions.FirstOrDefault(x => x.TeamID == position.Team.Id);
                                if(tournamentPosition != null)
                                {
                                    position.Qualified = tournamentPosition.Qualified;
                                    position.AsHosts = (tournamentToQualify.Host == position.Team.Name);
                                }
                            }
                        }
                    }
                }

                response.Rounds.AddRange(rounds);
            }

            return response;
        }

        public async Task<List<GroupPosition>> GetFinalTable(int id)
        {
            var response = new List<GroupPosition>();

            var tournament = await _tournamentRepository.GetWithoutPositions(id);
            if (tournament != null)
            {
                var matches = await _matchRepository.GetByTournament(id);
                if (matches.Count <= 0 || 
                    (tournament.TournamentType.Format == TournamentFormat.ConfederationTournament && 
                    !matches.Any(e => e.MatchRound == MatchRound.Group)))
                {
                    return null;
                }

                response = GetFinalStandingsByPointsOrRound(matches, tournament);
            }

            return response;
        }

        public Task<Tournament> Get(int id)
        {
            return _tournamentRepository.Get(id);
        }

        public async Task Add(Tournament tournament)
        {
            await _tournamentRepository.Add(tournament);
            await _tournamentRepository.SaveChanges();
        }

        public async Task Update(Tournament tournament)
        {
            _tournamentRepository.Update(tournament);
            await _tournamentRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _tournamentRepository.Delete(id);
            await _tournamentRepository.SaveChanges();
        }

        private string GetMatchRoundDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        private bool IsHomeAndAway(RoundMatches round)
        {
            if(round.Matches.Count != 2)
            {
                return false;
            }

            if((round.Matches[0].Team1ID != round.Matches[1].Team2ID) && (round.Matches[0].Team2ID != round.Matches[1].Team1ID))
            {
                return false;
            }

            return true;
        }

        private List<GroupPosition> GetFinalStandingsByPointsOrRound(List<Match> matches, Tournament tournament)
        {
            if(tournament.TournamentType.Format == TournamentFormat.ConfederationTournament)
            {
                matches = matches.Where(e => e.MatchRound != MatchRound.Playoff).ToList();
            }

            List<GroupPosition> positions = new List<GroupPosition>();
            foreach (Match match in matches)
            {
                GroupPosition positionTeam1 = positions.FirstOrDefault(e => e.Team.Id == match.Team1ID);
                if (positionTeam1 == null)
                {
                    positionTeam1 = new GroupPosition(match.Team1);
                    positions.Add(positionTeam1);
                }

                GroupPosition positionTeam2 = positions.FirstOrDefault(e => e.Team.Id == match.Team2ID);
                if (positionTeam2 == null)
                {
                    positionTeam2 = new GroupPosition(match.Team2);
                    positions.Add(positionTeam2);
                }

                switch (GetMatchResult(match))
                {
                    case MatchResult.Home:
                        positionTeam1.Wins++;
                        positionTeam2.Loses++;
                        break;
                    case MatchResult.Away:
                        positionTeam1.Loses++;
                        positionTeam2.Wins++;
                        break;
                    case MatchResult.Draw:
                        positionTeam1.Draws++;
                        positionTeam2.Draws++;
                        break;
                }

                positionTeam1.GoalsFavor += match.GoalsTeam1;
                positionTeam1.GoalsAgainst += match.GoalsTeam2;
                positionTeam2.GoalsFavor += match.GoalsTeam2;
                positionTeam2.GoalsAgainst += match.GoalsTeam1;

                if(positionTeam1.HighestRound < match.MatchRound)
                {
                    positionTeam1.HighestRound = match.MatchRound;
                    positionTeam1.Result = GetMatchRoundDescription(match.MatchRound);

                    if(tournament.TournamentType.Format == TournamentFormat.Qualification && match.MatchRound == MatchRound.Final)
                    {
                        positionTeam1.Result = "Play Offs";
                    }
                }

                if (positionTeam2.HighestRound < match.MatchRound)
                {
                    positionTeam2.HighestRound = match.MatchRound;
                    positionTeam2.Result = GetMatchRoundDescription(match.MatchRound);

                    if (tournament.TournamentType.Format == TournamentFormat.Qualification && match.MatchRound == MatchRound.Final)
                    {
                        positionTeam2.Result = "Play Offs";
                    }
                }
            }

            if(tournament.TournamentType.Format == TournamentFormat.Qualification)
            {
                positions = positions.OrderByDescending(e => e.Points)
                                    .ThenByDescending(e => e.GoalDifference)
                                    .ThenByDescending(e => e.GoalsFavor)
                                    .ThenBy(e => e.Team.Name)
                                    .ToList();
            }
            else
            {
                positions = positions.OrderByDescending(e => e.HighestRound)
                                    .ThenByDescending(e => e.Points)
                                    .ThenByDescending(e => e.GoalDifference)
                                    .ThenByDescending(e => e.GoalsFavor)
                                    .ThenBy(e => e.Team.Name)
                                    .ToList();
            }

            positions.ForEach(e => e.NoPosition = positions.IndexOf(e) + 1);

            if(tournament.TournamentType.Format != TournamentFormat.Qualification)
            {
                var thirdPlaceMatch = matches.FirstOrDefault(e => e.MatchRound == MatchRound.ThirdPlace);
                if (thirdPlaceMatch != null)
                {
                    var team1 = positions.FirstOrDefault(e => e.Team.Id == thirdPlaceMatch.Team1ID);
                    var team2 = positions.FirstOrDefault(e => e.Team.Id == thirdPlaceMatch.Team2ID);
                    if (thirdPlaceMatch.GoalsTeam1 > thirdPlaceMatch.GoalsTeam2 || (thirdPlaceMatch.PenaltiesTeam1 > thirdPlaceMatch.PenaltiesTeam2))
                    {
                        team1.NoPosition = 3;
                        team1.Result = "Tercero";
                        team2.NoPosition = 4;
                        team2.Result = "Cuarto";
                    }
                    else if (thirdPlaceMatch.GoalsTeam2 > thirdPlaceMatch.GoalsTeam1 || (thirdPlaceMatch.PenaltiesTeam2 > thirdPlaceMatch.PenaltiesTeam1))
                    {
                        team2.NoPosition = 3;
                        team2.Result = "Tercero";
                        team1.NoPosition = 4;
                        team1.Result = "Cuarto";
                    }
                }

                var finalMatch = matches.FirstOrDefault(e => e.MatchRound == MatchRound.Final);
                var finalteam1 = positions.FirstOrDefault(e => e.Team.Id == finalMatch.Team1ID);
                var finalteam2 = positions.FirstOrDefault(e => e.Team.Id == finalMatch.Team2ID);
                if (finalMatch.GoalsTeam1 > finalMatch.GoalsTeam2 || (finalMatch.PenaltiesTeam1 > finalMatch.PenaltiesTeam2))
                {
                    finalteam1.NoPosition = 1;
                    finalteam1.Result = "Campeón";
                    finalteam2.NoPosition = 2;
                    finalteam2.Result = "Sub Campeón";
                }
                else if (finalMatch.GoalsTeam2 > finalMatch.GoalsTeam1 || (finalMatch.PenaltiesTeam2 > finalMatch.PenaltiesTeam1))
                {
                    finalteam2.NoPosition = 1;
                    finalteam2.Result = "Campeón";
                    finalteam1.NoPosition = 2;
                    finalteam1.Result = "Sub Campeón";
                }
            }

            if(tournament.ConfederationID != null)
            {
                positions = positions.Where(e => e.Team.ConfederationID == tournament.ConfederationID).ToList();
            }

            return positions.OrderBy(e => e.NoPosition).ToList();
        }

        private MatchResult GetMatchResult(Match match)
        {
            MatchResult result = MatchResult.Draw;

            if(match.GoalsTeam1 > match.GoalsTeam2)
            {
                result = MatchResult.Home;
            }

            if(match.GoalsTeam2 > match.GoalsTeam1)
            {
                result = MatchResult.Away;
            }

            return result;
        }
    }
}
