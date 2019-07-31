using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class ConfederationService : IConfederationService
    {
        private readonly IConfederationRepository _confederationRepository;
        private readonly ITeamRepository _teamRepository;

        public ConfederationService(IConfederationRepository confederationRepository, ITeamRepository teamRepository)
        {
            this._confederationRepository = confederationRepository;
            this._teamRepository = teamRepository;
        }

        public Task<List<Confederation>> Get()
        {
            return _confederationRepository.Get();
        }

        public async Task<List<Team>> GetAllTeamsByConfederation(int confederationID)
        {
            return await _teamRepository.GetAllByConfederation(confederationID);
        }

        public Task<Confederation> Get(int id)
        {
            return _confederationRepository.Get(id);
        }

        public async Task Add(Confederation confederation)
        {
            await _confederationRepository.Add(confederation);
            await _confederationRepository.SaveChanges();
        }

        public async Task Update(Confederation confederation)
        {
            _confederationRepository.Update(confederation);
            await _confederationRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _confederationRepository.Delete(id);
            await _confederationRepository.SaveChanges();
        }
    }
}
