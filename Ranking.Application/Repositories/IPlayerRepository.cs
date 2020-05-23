using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IPlayerRepository
    {
        Task<List<Player>> Get();
        Task<List<Player>> GetByTeam(int teamId);
        Task<Player> Get(int id);
        Task Add(Player player);
        void Update(Player player);
        Task Delete(int id);
        Task SaveChanges();
    }
}
