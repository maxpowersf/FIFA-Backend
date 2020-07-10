using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> Get();
        Task<List<Player>> GetByTeam(int teamId);
        Task<List<Player>> GetPlayersWithGoals(int tournamentTypeId);
        Task<Player> Get(int id);
        Task Add(Player player);
        Task Update(Player player);
        Task Delete(int id);
    }
}
