using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            this._tournamentRepository = tournamentRepository;
        }

        public Task<List<Tournament>> Get()
        {
            return _tournamentRepository.Get();
        }

        public Task<List<Tournament>> GetByTeam(int teamId)
        {
            return _tournamentRepository.GetByTeam(teamId);
        }

        public Task<Tournament> Get(int id)
        {
            return _tournamentRepository.Get(id);
        }

        public async Task Add(Tournament tournament)
        {
            await _tournamentRepository.Add(tournament);
            await _tournamentRepository.SaveChanges();
        }

        public async Task Update(Tournament tournament)
        {
            _tournamentRepository.Update(tournament);
            await _tournamentRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _tournamentRepository.Delete(id);
            await _tournamentRepository.SaveChanges();
        }
    }
}
