using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class H2HService : IH2HService
    {
        private readonly IH2HRepository _h2HRepository;

        public H2HService(IH2HRepository h2HRepository)
        {
            this._h2HRepository = h2HRepository;
        }

        public async Task Add(Head2Head h2h)
        {
            await _h2HRepository.Add(h2h);
            await _h2HRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _h2HRepository.Delete(id);
            await _h2HRepository.SaveChanges();
        }

        public Task<List<Head2Head>> Get()
        {
            return _h2HRepository.Get();
        }

        public Task<Head2Head> Get(int id)
        {
            return _h2HRepository.Get(id);
        }

        public Task<List<Head2Head>> GetByTeam(int teamId)
        {
            return _h2HRepository.GetByTeam(teamId);
        }

        public Task<Head2Head> GetByTeams(int team1Id, int team2Id)
        {
            return _h2HRepository.GetByTeams(team1Id, team2Id);
        }

        public async Task Update(Head2Head h2h)
        {
            _h2HRepository.Update(h2h);
            await _h2HRepository.SaveChanges();
        }
    }
}
