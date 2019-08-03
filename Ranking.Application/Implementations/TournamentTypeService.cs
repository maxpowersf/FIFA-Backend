using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class TournamentTypeService : ITournamentTypeService
    {
        private readonly ITournamentTypeRepository _tournamentTypeRepository;

        public TournamentTypeService(ITournamentTypeRepository tournamentTypeRepository)
        {
            this._tournamentTypeRepository = tournamentTypeRepository;
        }

        public Task<List<TournamentType>> Get()
        {
            return _tournamentTypeRepository.Get();
        }

        public Task<TournamentType> Get(int id)
        {
            return _tournamentTypeRepository.Get(id);
        }

        public async Task Add(TournamentType tournamentType)
        {
            await _tournamentTypeRepository.Add(tournamentType);
            await _tournamentTypeRepository.SaveChanges();
        }

        public async Task Update(TournamentType tournamentType)
        {
            _tournamentTypeRepository.Update(tournamentType);
            await _tournamentTypeRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _tournamentTypeRepository.Delete(id);
            await _tournamentTypeRepository.SaveChanges();
        }
    }
}
