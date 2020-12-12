using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IH2HService
    {
        Task<List<Head2Head>> Get();
        Task<List<Head2Head>> GetByTeam(int teamId);
        Task<Head2Head> GetByTeams(int team1Id, int team2Id);
        Task<Head2Head> Get(int id);
        Task Add(Head2Head h2h);
        Task Update(Head2Head h2h);
        Task Delete(int id);
    }
}
