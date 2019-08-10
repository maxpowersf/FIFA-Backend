using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IPositionRepository
    {
        Task<List<Position>> Get();
        Task<List<Position>> GetByTeam(int id);
        Task<List<Position>> GetByTournament(int id);
        Task<Position> Get(int id);
        Task Add(Position position);
        void Update(Position position);
        Task Delete(int id);
        Task SaveChanges();
    }
}
