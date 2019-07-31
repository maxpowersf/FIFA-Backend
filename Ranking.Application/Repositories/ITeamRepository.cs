using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> Get();
        List<Team> GetOrdered();
        Task<List<Team>> GetAllByConfederation(int confederationID);
        Task<Team> Get(int id);
        Task Add(Team team);
        void Update(Team team);
        Task Delete(int id);
        Task SaveChanges();
    }
}
