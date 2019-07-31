using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IMatchTypeRepository
    {
        Task<List<MatchType>> Get();
        Task<MatchType> Get(int id);
        Task Add(MatchType matchType);
        void Update(MatchType matchType);
        Task Delete(int id);
        Task SaveChanges();
    }
}
