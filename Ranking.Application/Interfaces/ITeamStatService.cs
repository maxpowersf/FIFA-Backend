using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface ITeamStatService
    {
        Task<List<TeamStat>> Get();
        Task<List<TeamStat>> GetByTeam(int teamId);
        Task<TeamStat> Get(int id);
        Task Add(TeamStat teamStat);
        Task Update(TeamStat teamStat);
    }
}
