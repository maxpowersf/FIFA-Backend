using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using Ranking.Domain.Request;
using Ranking.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;

        public MatchService(IMatchRepository matchRepository,
                            ITeamRepository teamRepository)
        {
            this._matchRepository = matchRepository;
            this._teamRepository = teamRepository;
        }

        public async Task Add(Match match)
        {
            await _matchRepository.Add(match);
            await _matchRepository.SaveChanges();
        }

        public Task<List<Match>> Get()
        {
            return _matchRepository.Get();
        }

        public Task<List<Match>> Get(MatchCollectionRequest request)
        {
            return _matchRepository.Get(request);
        }

        public Task<Match> Get(int id)
        {
            return _matchRepository.Get(id);
        }

        public async Task<List<Match>> GetByTournament(int id)
        {
            return await _matchRepository.GetByTournament(id);
        }

        public Task<List<Match>> GetByTeam(int teamId)
        {
            return _matchRepository.GetByTeam(teamId);
        }

        public Task<List<Match>> GetByTeams(int team1Id, int team2Id, bool worldcup)
        {
            return _matchRepository.GetByTeams(team1Id, team2Id, worldcup);
        }

        public async Task Update(Match match)
        {
            _matchRepository.Update(match);
            await _matchRepository.SaveChanges();
        }

        public async Task<List<Match>> GetReportMargin()
        {
            return await _matchRepository.GetReportMargin();
        }

        public async Task<List<Match>> GetReportGoals()
        {
            return await _matchRepository.GetReportGoals();
        }

        public async Task<List<StreakCollectionResponse>> GetReportWinning()
        {
            int amount = 20;
            var response = new List<StreakCollectionResponse>();

            var teams = await _teamRepository.Get();
            foreach (var team in teams)
            {
                int streak = 0;
                var streakMatches = new List<Match>();

                var matches = await _matchRepository.GetByTeam(team.Id);
                foreach (var match in matches.OrderBy(e => e.Date))
                {
                    if ((match.Team1ID == team.Id && match.GoalsTeam1 > match.GoalsTeam2) ||
                        (match.Team2ID == team.Id && match.GoalsTeam2 > match.GoalsTeam1))
                    {
                        streak++;
                        streakMatches.Add(match);
                    }
                    else
                    {
                        if (streak > 0)
                        {
                            var responseItem = new StreakCollectionResponse
                            {
                                Team = team,
                                Streak = streak,
                                IsCurrent = false,
                                Matches = new List<Match>()
                            };
                            responseItem.Matches.AddRange(streakMatches);

                            response.Add(responseItem);
                        }

                        streak = 0;
                        streakMatches.Clear();
                    }
                }

                if (streak > 0)
                {
                    var responseItem = new StreakCollectionResponse
                    {
                        Team = team,
                        Streak = streak,
                        IsCurrent = true,
                        Matches = new List<Match>()
                    };
                    responseItem.Matches.AddRange(streakMatches);

                    response.Add(responseItem);
                }
            }

            return response.OrderByDescending(e => e.Streak).Take(amount).ToList();
        }

        public async Task<List<StreakCollectionResponse>> GetReportUnbeaten()
        {
            int amount = 20;
            var response = new List<StreakCollectionResponse>();

            var teams = await _teamRepository.Get();
            foreach(var team in teams)
            {
                int streak = 0;
                var streakMatches = new List<Match>();

                var matches = await _matchRepository.GetByTeam(team.Id);
                foreach(var match in matches.OrderBy(e => e.Date))
                {
                    if((match.Team1ID == team.Id && match.GoalsTeam1 >= match.GoalsTeam2) ||
                        (match.Team2ID == team.Id && match.GoalsTeam2 >= match.GoalsTeam1))
                    {
                        streak++;
                        streakMatches.Add(match);
                    }
                    else
                    {
                        if(streak > 10)
                        {
                            var responseItem = new StreakCollectionResponse
                            {
                                Team = team,
                                Streak = streak,
                                IsCurrent = false,
                                Matches = new List<Match>()
                            };
                            responseItem.Matches.AddRange(streakMatches);

                            response.Add(responseItem);
                        }

                        streak = 0;
                        streakMatches.Clear();
                    }
                }

                if(streak > 10)
                {
                    var responseItem = new StreakCollectionResponse
                    {
                        Team = team,
                        Streak = streak,
                        IsCurrent = true,
                        Matches = new List<Match>()
                    };
                    responseItem.Matches.AddRange(streakMatches);

                    response.Add(responseItem);
                }
            }

            return response.OrderByDescending(e => e.Streak).Take(amount).ToList();
        }

        public async Task<List<StreakCollectionResponse>> GetReportLosing()
        {
            int amount = 20;
            var response = new List<StreakCollectionResponse>();

            var teams = await _teamRepository.Get();
            foreach (var team in teams)
            {
                int streak = 0;
                var streakMatches = new List<Match>();

                var matches = await _matchRepository.GetByTeam(team.Id);
                foreach (var match in matches.OrderBy(e => e.Date))
                {
                    if ((match.Team1ID == team.Id && match.GoalsTeam1 < match.GoalsTeam2) ||
                        (match.Team2ID == team.Id && match.GoalsTeam2 < match.GoalsTeam1))
                    {
                        streak++;
                        streakMatches.Add(match);
                    }
                    else
                    {
                        if (streak > 0)
                        {
                            var responseItem = new StreakCollectionResponse
                            {
                                Team = team,
                                Streak = streak,
                                IsCurrent = false,
                                Matches = new List<Match>()
                            };
                            responseItem.Matches.AddRange(streakMatches);

                            response.Add(responseItem);
                        }

                        streak = 0;
                        streakMatches.Clear();
                    }
                }

                if (streak > 0)
                {
                    var responseItem = new StreakCollectionResponse
                    {
                        Team = team,
                        Streak = streak,
                        IsCurrent = true,
                        Matches = new List<Match>()
                    };
                    responseItem.Matches.AddRange(streakMatches);

                    response.Add(responseItem);
                }
            }

            return response.OrderByDescending(e => e.Streak).Take(amount).ToList();
        }

        public async Task<List<StreakCollectionResponse>> GetReportWinningless()
        {
            int amount = 20;
            var response = new List<StreakCollectionResponse>();

            var teams = await _teamRepository.Get();
            foreach (var team in teams)
            {
                int streak = 0;
                var streakMatches = new List<Match>();

                var matches = await _matchRepository.GetByTeam(team.Id);
                foreach (var match in matches.OrderBy(e => e.Date))
                {
                    if ((match.Team1ID == team.Id && match.GoalsTeam1 <= match.GoalsTeam2) ||
                        (match.Team2ID == team.Id && match.GoalsTeam2 <= match.GoalsTeam1))
                    {
                        streak++;
                        streakMatches.Add(match);
                    }
                    else
                    {
                        if (streak > 10)
                        {
                            var responseItem = new StreakCollectionResponse
                            {
                                Team = team,
                                Streak = streak,
                                IsCurrent = false,
                                Matches = new List<Match>()
                            };
                            responseItem.Matches.AddRange(streakMatches);

                            response.Add(responseItem);
                        }

                        streak = 0;
                        streakMatches.Clear();
                    }
                }

                if (streak > 10)
                {
                    var responseItem = new StreakCollectionResponse
                    {
                        Team = team,
                        Streak = streak,
                        IsCurrent = true,
                        Matches = new List<Match>()
                    };
                    responseItem.Matches.AddRange(streakMatches);

                    response.Add(responseItem);
                }
            }

            return response.OrderByDescending(e => e.Streak).Take(amount).ToList();
        }
    }
}
