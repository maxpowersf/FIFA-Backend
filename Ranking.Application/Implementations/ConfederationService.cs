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

        public ConfederationService(IConfederationRepository confederationRepository)
        {
            this._confederationRepository = confederationRepository;
        }

        public Task<List<Confederation>> Get()
        {
            return _confederationRepository.Get();
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
