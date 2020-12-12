using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class H2HWorldCupService : IH2HWorldCupService
    {
        private readonly IH2HWorldCupRepository _h2HWorldCupRepository;

        public H2HWorldCupService(IH2HWorldCupRepository h2HWorldCupRepository)
        {
            this._h2HWorldCupRepository = h2HWorldCupRepository;
        }

        public async Task Add(Head2HeadWorldCup h2hWorldCup)
        {
            await _h2HWorldCupRepository.Add(h2hWorldCup);
            await _h2HWorldCupRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _h2HWorldCupRepository.Delete(id);
            await _h2HWorldCupRepository.SaveChanges();
        }

        public Task<List<Head2HeadWorldCup>> Get()
        {
            return _h2HWorldCupRepository.Get();
        }

        public Task<Head2HeadWorldCup> Get(int id)
        {
            return _h2HWorldCupRepository.Get(id);
        }

        public Task<List<Head2HeadWorldCup>> GetByTeam(int teamId)
        {
            return _h2HWorldCupRepository.GetByTeam(teamId);
        }

        public Task<Head2HeadWorldCup> GetByTeams(int team1Id, int team2Id)
        {
            return _h2HWorldCupRepository.GetByTeams(team1Id, team2Id);
        }

        public async Task Update(Head2HeadWorldCup h2hWorldCup)
        {
            _h2HWorldCupRepository.Update(h2hWorldCup);
            await _h2HWorldCupRepository.SaveChanges();
        }
    }
}
