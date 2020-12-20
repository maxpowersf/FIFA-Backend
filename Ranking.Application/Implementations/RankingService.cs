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
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamStatRepository _teamStatRepository;
        private readonly ITeamStatWorldCupRepository _teamStatWorldCupRepository;
        private readonly IH2HRepository _h2hRepository;
        private readonly IH2HWorldCupRepository _h2hWorldCupRepository;
        private readonly IMatchRepository _matchRepository;

        public RankingService(IRankingRepository rankingRepository, 
            ITeamRepository teamRepository, 
            ITournamentRepository tournamentRepository,
            ITeamStatRepository teamStatRepository,
            ITeamStatWorldCupRepository teamStatWorldCupRepository,
            IH2HRepository h2hRepository,
            IH2HWorldCupRepository h2hWorldCupRepository,
            IMatchRepository matchRepository)
        {
            this._rankingRepository = rankingRepository;
            this._teamRepository = teamRepository;
            this._tournamentRepository = tournamentRepository;
            this._teamStatRepository = teamStatRepository;
            this._teamStatWorldCupRepository = teamStatWorldCupRepository;
            this._h2hRepository = h2hRepository;
            this._h2hWorldCupRepository = h2hWorldCupRepository;
            this._matchRepository = matchRepository;
        }

        public async Task AddMatch(Match match)
        {
            match.MatchResult = GetMatchResult(match);

            Tournament tournament = await _tournamentRepository.Get(match.TournamentID);
            Team team1 = await _teamRepository.Get(match.Team1ID);
            Team team2 = await _teamRepository.Get(match.Team2ID);

            #region Update Team Stats
            var team1Stat = _teamStatRepository.GetOrCreateByTeam(match.Team1ID);
            var team2Stat = _teamStatRepository.GetOrCreateByTeam(match.Team2ID);

            CompleteTeamsStat(match, ref team1Stat, ref team2Stat);

            _teamStatRepository.Update(team1Stat);
            _teamStatRepository.Update(team2Stat);

            if (tournament.TournamentType.Format == TournamentFormat.WorldCup)
            {
                var team1StatWorldCup = _teamStatWorldCupRepository.GetOrCreateByTeam(match.Team1ID);
                var team2StatWorldCup = _teamStatWorldCupRepository.GetOrCreateByTeam(match.Team2ID);

                CompleteTeamsStatWorldCup(match, ref team1StatWorldCup, ref team2StatWorldCup);

                _teamStatWorldCupRepository.Update(team1StatWorldCup);
                _teamStatWorldCupRepository.Update(team2StatWorldCup);
            }
            #endregion

            #region Update Head 2 Head
            var h2hTeam1 = _h2hRepository.GetOrCreateByTeams(match.Team1ID, match.Team2ID);
            var h2hTeam2 = _h2hRepository.GetOrCreateByTeams(match.Team2ID, match.Team1ID);

            CompleteH2H(match, ref h2hTeam1, ref h2hTeam2);

            _h2hRepository.Update(h2hTeam1);
            _h2hRepository.Update(h2hTeam2);

            if (tournament.TournamentType.Format == TournamentFormat.WorldCup)
            {
                var h2hTeam1WorldCup = _h2hWorldCupRepository.GetOrCreateByTeams(match.Team1ID, match.Team2ID);
                var h2hTeam2WorldCup = _h2hWorldCupRepository.GetOrCreateByTeams(match.Team2ID, match.Team1ID);

                CompleteH2HWorldCup(match, ref h2hTeam1WorldCup, ref h2hTeam2WorldCup);

                _h2hWorldCupRepository.Update(h2hTeam1WorldCup);
                _h2hWorldCupRepository.Update(h2hTeam2WorldCup);
            }
            #endregion

            #region Update Rankings
            var regionalWeight = GetRegionalWeight(team1.Confederation.Weight, team2.Confederation.Weight);
            var team1Points = GetTeamPoints(GetTeamResult(match, 1), regionalWeight, CalculateOposition(team2.ActualRank), tournament.TournamentType.MatchType.Weight);
            var team2Points = GetTeamPoints(GetTeamResult(match, 2), regionalWeight, CalculateOposition(team1.ActualRank), tournament.TournamentType.MatchType.Weight);

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

            await _matchRepository.Add(match);

            await _teamRepository.SaveChanges();
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

        private void CompleteTeamsStat(Match match, ref TeamStat team1Stat, ref TeamStat team2Stat)
        {
            if(match.GoalsTeam1 > match.GoalsTeam2)
            {
                team1Stat.Wins++;
                team2Stat.Loses++;
            }
            else if(match.GoalsTeam2 > match.GoalsTeam1)
            {
                team2Stat.Wins++;
                team1Stat.Loses++;
            }
            else
            {
                team1Stat.Draws++;
                team2Stat.Draws++;
            }

            team1Stat.Points = team1Stat.Wins * 3 + team1Stat.Draws;
            team1Stat.GamesPlayed++;
            team1Stat.GoalsFavor += match.GoalsTeam1;
            team1Stat.GoalsAgainst += match.GoalsTeam2;
            team1Stat.GoalDifference = team1Stat.GoalsFavor - team1Stat.GoalsAgainst;
            team1Stat.Effectiveness = Math.Round(((decimal)team1Stat.Points / ((decimal)team1Stat.GamesPlayed * 3)) * 100, 2);

            team2Stat.Points = team2Stat.Wins * 3 + team2Stat.Draws;
            team2Stat.GamesPlayed++;
            team2Stat.GoalsFavor += match.GoalsTeam2;
            team2Stat.GoalsAgainst += match.GoalsTeam1;
            team2Stat.GoalDifference = team2Stat.GoalsFavor - team2Stat.GoalsAgainst;
            team2Stat.Effectiveness = Math.Round(((decimal)team2Stat.Points / ((decimal)team2Stat.GamesPlayed * 3)) * 100, 2);
        }

        private void CompleteTeamsStatWorldCup(Match match, ref TeamStatWorldCup team1Stat, ref TeamStatWorldCup team2Stat)
        {
            if (match.GoalsTeam1 > match.GoalsTeam2)
            {
                team1Stat.Wins++;
                team2Stat.Loses++;
            }
            else if (match.GoalsTeam2 > match.GoalsTeam1)
            {
                team2Stat.Wins++;
                team1Stat.Loses++;
            }
            else
            {
                team1Stat.Draws++;
                team2Stat.Draws++;
            }

            team1Stat.Points = team1Stat.Wins * 3 + team1Stat.Draws;
            team1Stat.GamesPlayed++;
            team1Stat.GoalsFavor += match.GoalsTeam1;
            team1Stat.GoalsAgainst += match.GoalsTeam2;
            team1Stat.GoalDifference = team1Stat.GoalsFavor - team1Stat.GoalsAgainst;
            team1Stat.Effectiveness = Math.Round(((decimal)team1Stat.Points / ((decimal)team1Stat.GamesPlayed * 3)) * 100, 2);

            team2Stat.Points = team2Stat.Wins * 3 + team2Stat.Draws;
            team2Stat.GamesPlayed++;
            team2Stat.GoalsFavor += match.GoalsTeam2;
            team2Stat.GoalsAgainst += match.GoalsTeam1;
            team2Stat.GoalDifference = team2Stat.GoalsFavor - team2Stat.GoalsAgainst;
            team2Stat.Effectiveness = Math.Round(((decimal)team2Stat.Points / ((decimal)team2Stat.GamesPlayed * 3)) * 100, 2);
        }

        private void CompleteH2H(Match match, ref Head2Head h2hTeam1, ref Head2Head h2hTeam2)
        {
            if (match.GoalsTeam1 > match.GoalsTeam2)
            {
                h2hTeam1.Wins++;
                h2hTeam2.Loses++;
            }
            else if (match.GoalsTeam2 > match.GoalsTeam1)
            {
                h2hTeam2.Wins++;
                h2hTeam1.Loses++;
            }
            else
            {
                h2hTeam1.Draws++;
                h2hTeam2.Draws++;
            }

            h2hTeam1.GamesPlayed++;
            h2hTeam1.GoalsFavor += match.GoalsTeam1;
            h2hTeam1.GoalsAgainst += match.GoalsTeam2;
            
            h2hTeam2.GamesPlayed++;
            h2hTeam2.GoalsFavor += match.GoalsTeam2;
            h2hTeam2.GoalsAgainst += match.GoalsTeam1;
        }

        private void CompleteH2HWorldCup(Match match, ref Head2HeadWorldCup h2hTeam1, ref Head2HeadWorldCup h2hTeam2)
        {
            if (match.GoalsTeam1 > match.GoalsTeam2)
            {
                h2hTeam1.Wins++;
                h2hTeam2.Loses++;
            }
            else if (match.GoalsTeam2 > match.GoalsTeam1)
            {
                h2hTeam2.Wins++;
                h2hTeam1.Loses++;
            }
            else
            {
                h2hTeam1.Draws++;
                h2hTeam2.Draws++;
            }

            h2hTeam1.GamesPlayed++;
            h2hTeam1.GoalsFavor += match.GoalsTeam1;
            h2hTeam1.GoalsAgainst += match.GoalsTeam2;

            h2hTeam2.GamesPlayed++;
            h2hTeam2.GoalsFavor += match.GoalsTeam2;
            h2hTeam2.GoalsAgainst += match.GoalsTeam1;
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
