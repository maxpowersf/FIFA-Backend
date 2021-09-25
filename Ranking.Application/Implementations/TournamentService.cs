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

        public TournamentService(ITournamentRepository tournamentRepository, ITeamRepository teamRepository, IMatchRepository matchRepository)
        {
            this._tournamentRepository = tournamentRepository;
            this._teamRepository = teamRepository;
            this._matchRepository = matchRepository;
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

                if(tournament.TournamentType.Format == TournamentFormat.Qualification && rounds.Count == 1)
                {
                    rounds.FirstOrDefault().RoundName = "Play Offs";
                    rounds.FirstOrDefault().IsHomeAway = IsHomeAndAway(rounds.FirstOrDefault());
                }

                response.Rounds.AddRange(rounds);
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
    }
}
