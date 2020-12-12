using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class TeamStatWorldCupService : ITeamStatWorldCupService
    {
        private readonly ITeamStatWorldCupRepository _teamStatWorldCupRepository;

        public TeamStatWorldCupService(ITeamStatWorldCupRepository teamStatWorldCupRepository)
        {
            this._teamStatWorldCupRepository = teamStatWorldCupRepository;
        }

        public async Task Add(TeamStatWorldCup teamStat)
        {
            await _teamStatWorldCupRepository.Add(teamStat);
            await _teamStatWorldCupRepository.SaveChanges();
        }

        public Task<List<TeamStatWorldCup>> Get()
        {
            return _teamStatWorldCupRepository.Get();
        }

        public Task<TeamStatWorldCup> Get(int id)
        {
            return _teamStatWorldCupRepository.Get(id);
        }

        public Task<List<TeamStatWorldCup>> GetByTeam(int teamId)
        {
            return _teamStatWorldCupRepository.GetByTeam(teamId);
        }

        public async Task Update(TeamStatWorldCup teamStat)
        {
            _teamStatWorldCupRepository.Update(teamStat);
            await _teamStatWorldCupRepository.SaveChanges();
        }
    }
}
