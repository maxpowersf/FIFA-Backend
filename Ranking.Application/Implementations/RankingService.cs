using AutoMapper;
using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
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

        public RankingService(IRankingRepository rankingRepository, ITeamRepository teamRepository)
        {
            this._rankingRepository = rankingRepository;
            this._teamRepository = teamRepository;
        }

        public async Task Add(Match match)
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
    }
}
