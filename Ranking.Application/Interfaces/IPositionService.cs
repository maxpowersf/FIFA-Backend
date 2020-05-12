using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IPositionService
    {
        Task<List<Position>> Get();
        Task<List<Position>> GetByTeam(int id);
        Task<List<Position>> GetByTournament(int id);
        Task<Position> Get(int id);
        Task Add(List<Position> positions);
        Task Update(Position position);
        Task Delete(int id);
    }
}
