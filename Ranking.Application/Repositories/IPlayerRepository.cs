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
        Task<List<Player>> GetWorldCupGoals();
        Task<List<Player>> GetConfederationsCupGoals();
        Task<List<Player>> GetConfederationTournamentGoals(int confederationID);
        Task<List<Player>> GetQualificationGoals();
        Task<Player> Get(int id);
        Task<Player> Get(string name, string teamName);
        Task Add(Player player);
        void Update(Player player);
        Task Delete(int id);
        Task SaveChanges();
    }
}
