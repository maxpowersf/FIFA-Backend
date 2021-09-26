using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> Get();
        Task<List<Tournament>> GetByTeam(int teamId, int confederationId);
        Task<List<Tournament>> GetByTournamentTypeWithPositions(int tournamentTypeId);
        Task<List<Tournament>> GetByTournamentTypeAndConfederation(int tournamentTypeId, int confederationId);
        Task<Tournament> GetByQualificationYear(int year);
        Task<Tournament> GetWithoutPositions(int id);
        Task<Tournament> Get(int id);
        Task Add(Tournament tournament);
        void Update(Tournament tournament);
        Task Delete(int id);
        Task SaveChanges();
    }
}
