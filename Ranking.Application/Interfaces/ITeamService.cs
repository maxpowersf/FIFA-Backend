using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> Get();
        Task<List<Team>> GetAllTeamsByConfederation(int confederationID);
        Task<List<Team>> GetFirstTeams(int quantity);
        Task<List<Team>> GetTeamsWithTitles(int tournamenttypeID, int quantity);
        Task<Team> Get(int id);
        Task Add(Team team);
        Task Update(Team team);
        Task Delete(int id);
        Task UpdateRankings();
    }
}
