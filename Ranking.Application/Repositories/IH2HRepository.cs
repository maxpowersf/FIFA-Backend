using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IH2HRepository
    {
        Task<List<Head2Head>> Get();
        Task<List<Head2Head>> GetByTeam(int teamId);
        Head2Head GetOrCreateByTeams(int team1Id, int team2Id);
        Task<Head2Head> GetByTeams(int team1Id, int team2Id);
        Task<Head2Head> Get(int id);
        Task Add(Head2Head h2h);
        void Update(Head2Head h2h);
        Task Delete(int id);
        Task SaveChanges();
    }
}
