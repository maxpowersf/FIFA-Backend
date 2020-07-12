using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> Get();
        Task<List<Tournament>> GetByTeam(int teamId);
        Task<List<Tournament>> GetByTournamentType(int tournamentTypeId);
        Task<Tournament> Get(int id);
        Task Add(Tournament tournament);
        Task Update(Tournament tournament);
        Task Delete(int id);
    }
}
