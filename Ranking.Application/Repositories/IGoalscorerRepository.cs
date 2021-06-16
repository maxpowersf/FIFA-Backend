using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IGoalscorerRepository
    {
        Task<List<Goalscorer>> Get();
        Task<List<Goalscorer>> GetByTournament(int id);
        Task<Goalscorer> GetByPlayerAndTournament(int playerId, int tournamentId);
        Task<Goalscorer> Get(int id);
        Task Add(Goalscorer goalscorer);
        void Update(Goalscorer goalscorer);
        Task Delete(int id);
        Task SaveChanges();
    }
}
