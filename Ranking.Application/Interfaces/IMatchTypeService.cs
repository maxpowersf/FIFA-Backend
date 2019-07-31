using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IMatchTypeService
    {
        Task<List<MatchType>> Get();
        Task<MatchType> Get(int id);
        Task Add(MatchType matchType);
        Task Update(MatchType matchType);
        Task Delete(int id);
    }
}
