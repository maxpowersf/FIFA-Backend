using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IRankingRepository
    {
        Task<Domain.Ranking> GetActual(int teamId);
        void Update(Domain.Ranking ranking);
        Task Add(Domain.Ranking ranking);
        Task SaveChanges();
    }
}
