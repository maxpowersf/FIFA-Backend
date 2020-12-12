using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface ITeamStatWorldCupService
    {
        Task<List<TeamStatWorldCup>> Get();
        Task<List<TeamStatWorldCup>> GetByTeam(int teamId);
        Task<TeamStatWorldCup> Get(int id);
        Task Add(TeamStatWorldCup teamStat);
        Task Update(TeamStatWorldCup teamStat);
    }
}
