using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class TeamStatService : ITeamStatService
    {
        private readonly ITeamStatRepository _teamStatRepository;

        public TeamStatService(ITeamStatRepository teamStatRepository)
        {
            this._teamStatRepository = teamStatRepository;
        }

        public async Task Add(TeamStat teamStat)
        {
            await _teamStatRepository.Add(teamStat);
            await _teamStatRepository.SaveChanges();
        }

        public Task<List<TeamStat>> Get()
        {
            return _teamStatRepository.Get();
        }

        public Task<TeamStat> Get(int id)
        {
            return _teamStatRepository.Get(id);
        }

        public Task<List<TeamStat>> GetByTeam(int teamId)
        {
            return _teamStatRepository.GetByTeam(teamId);
        }

        public async Task Update(TeamStat teamStat)
        {
            _teamStatRepository.Update(teamStat);
            await _teamStatRepository.SaveChanges();
        }
    }
}
