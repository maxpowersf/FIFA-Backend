using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITeamStatRepository
    {
        Task<List<TeamStat>> Get();
        Task<List<TeamStat>> GetByTeam(int teamId);
        TeamStat GetOrCreateByTeam(int teamId);
        Task<TeamStat> Get(int id);
        Task Add(TeamStat teamStat);
        void Update(TeamStat teamStat);
        Task SaveChanges();
    }
}
