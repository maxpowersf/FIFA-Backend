using AutoMapper;
using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class RankingService : IRankingService
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchTypeRepository _matchTypeRepository;

        public RankingService(IRankingRepository rankingRepository, ITeamRepository teamRepository, IMatchTypeRepository matchTypeRepository)
        {
            this._rankingRepository = rankingRepository;
            this._teamRepository = teamRepository;
            this._matchTypeRepository = matchTypeRepository;
        }

        public async Task AddMatchRanking(MatchRanking match)
        {
            Domain.Ranking ranking1 = await _rankingRepository.GetActual(match.Team1Id);
            ranking1.Points += match.Team1Points;
            _rankingRepository.Update(ranking1);

            Team team1 = await _teamRepository.Get(match.Team1Id);
            team1.TotalPoints += match.Team1Points;
            _teamRepository.Update(team1);

            Domain.Ranking ranking2 = await _rankingRepository.GetActual(match.Team2Id);
            ranking2.Points += match.Team2Points;
            _rankingRepository.Update(ranking2);

            Team team2 = await _teamRepository.Get(match.Team2Id);
            team2.TotalPoints += match.Team2Points;
            _teamRepository.Update(team2);

            await _rankingRepository.SaveChanges();
        }

        public async Task AddMatch(Match match)
        {
            match.MatchResult = GetMatchResult(match);

            MatchType matchType = await _matchTypeRepository.Get(match.MatchTypeID);
            Team team1 = await _teamRepository.Get(match.Team1ID);
            Team team2 = await _teamRepository.Get(match.Team2ID);

            #region Update Rankings
            var regionalWeight = GetRegionalWeight(team1.Confederation.Weight, team2.Confederation.Weight);
            var team1Points = GetTeamPoints(GetTeamResult(match, 1), regionalWeight, CalculateOposition(team2.ActualRank), matchType.Weight);
            var team2Points = GetTeamPoints(GetTeamResult(match, 2), regionalWeight, CalculateOposition(team1.ActualRank), matchType.Weight);

            Domain.Ranking ranking1 = await _rankingRepository.GetActual(match.Team1ID);
            ranking1.Points += team1Points;
            _rankingRepository.Update(ranking1);

            Domain.Ranking ranking2 = await _rankingRepository.GetActual(match.Team2ID);
            ranking2.Points += team2Points;
            _rankingRepository.Update(ranking2);

            team1.TotalPoints += team1Points;
            _teamRepository.Update(team1);

            team2.TotalPoints += team2Points;
            _teamRepository.Update(team2);
            #endregion

            #region Update Team Stats
            //Save Team Stats
            #endregion

            //Save Match
        }

        public async Task FinishPeriod()
        {
            List<Team> teams = await _teamRepository.Get();
            foreach(Team team in teams)
            {
                int year = team.Ranking3.Year + 4;
                decimal newPoints = (team.Ranking2.Points * Convert.ToDecimal(0.25)) + (team.Ranking3.Points * Convert.ToDecimal(0.5));

                Domain.Ranking ranking = new Domain.Ranking()
                {
                    TeamID = team.Id,
                    Year = year,
                    Points = 0
                };

                await _rankingRepository.Add(ranking);

                team.TotalPoints = newPoints;
                _teamRepository.Update(team);
            }

            await _rankingRepository.SaveChanges();
        }

        private decimal GetTeamPoints(decimal result, decimal regional, decimal oposition, decimal matchWeight)
        {
            var points = (result * regional * oposition * matchWeight) * 100;

            return Math.Round(points);
        }

        private decimal GetTeamResult(Match match, int team)
        {
            var result = 0;

            switch (team)
            {
                case 1:
                    if(match.MatchResult == MatchResult.Draw)
                    {
                        result = 1;
                    }
                    else if(match.MatchResult == MatchResult.Home)
                    {
                        result = (match.PenaltiesTeam1 == 0 && match.PenaltiesTeam2 == 0) ? 3 : 2;
                    }
                    else if(match.MatchResult == MatchResult.Away)
                    {
                        result = (match.PenaltiesTeam1 == 0 && match.PenaltiesTeam2 == 0) ? 0 : 1;
                    }
                    break;
                case 2:
                    if (match.MatchResult == MatchResult.Draw)
                    {
                        result = 1;
                    }
                    else if (match.MatchResult == MatchResult.Away)
                    {
                        result = (match.PenaltiesTeam1 == 0 && match.PenaltiesTeam2 == 0) ? 3 : 2;
                    }
                    else if (match.MatchResult == MatchResult.Home)
                    {
                        result = (match.PenaltiesTeam1 == 0 && match.PenaltiesTeam2 == 0) ? 0 : 1;
                    }
                    break;
            }

            return result;
        }

        private decimal GetRegionalWeight(decimal confederation1Weight, decimal confederation2Weight)
        {
            var regional = (confederation1Weight + confederation2Weight) / 2;

            return regional;
        }

        private decimal CalculateOposition(int teamRank)
        {
            decimal oposition = (200 - (decimal)teamRank) / 100;

            return oposition;
        }

        private MatchResult GetMatchResult(Match match)
        {
            if(match.GoalsTeam1 > match.GoalsTeam2)
            {
                return MatchResult.Home;
            }
            else if(match.GoalsTeam2 > match.GoalsTeam1)
            {
                return MatchResult.Away;
            }
            else
            {
                if(match.PenaltiesTeam1 > match.PenaltiesTeam2)
                {
                    return MatchResult.Home;
                }
                else if(match.PenaltiesTeam2 > match.PenaltiesTeam1)
                {
                    return MatchResult.Away;
                }
                else
                {
                    return MatchResult.Draw;
                }
            }
        }
    }
}
