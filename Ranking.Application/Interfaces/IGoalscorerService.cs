using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IGoalscorerService
    {
        Task<List<Goalscorer>> Get();
        Task<List<Goalscorer>> GetByTournament(int id);
        Task<Goalscorer> Get(int id);
        Task Add(List<Goalscorer> goalscorers);
        Task Update(Goalscorer goalscorer);
        Task Delete(int id);
    }
}
