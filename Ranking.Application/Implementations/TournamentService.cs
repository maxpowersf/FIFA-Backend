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
        private readonly ITeamRepository _teamRepository;

        public TournamentService(ITournamentRepository tournamentRepository, ITeamRepository teamRepository)
        {
            this._tournamentRepository = tournamentRepository;
            this._teamRepository = teamRepository;
        }

        public Task<List<Tournament>> Get()
        {
            return _tournamentRepository.Get();
        }

        public async Task<List<Tournament>> GetByTeam(int teamId)
        {
            Team team = await _teamRepository.Get(teamId);
            List<Tournament> tournamentList = await _tournamentRepository.GetByTeam(team.Id, team.ConfederationID);
            return tournamentList;
        }

        public async Task<List<Tournament>> GetByTournamentType(int TournamentTypeId)
        {
            List<Tournament> tournamentList = await _tournamentRepository.GetByTournamentType(TournamentTypeId);
            return tournamentList;
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
