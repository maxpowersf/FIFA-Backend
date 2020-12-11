using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITeamStatWorldCupRepository
    {
        Task<List<TeamStatWorldCup>> Get();
        Task<List<TeamStatWorldCup>> GetByTeam(int teamId);
        TeamStatWorldCup GetOrCreateByTeam(int teamId);
        Task<TeamStatWorldCup> Get(int id);
        Task Add(TeamStatWorldCup teamStat);
        void Update(TeamStatWorldCup teamStat);
        Task SaveChanges();
    }
}
