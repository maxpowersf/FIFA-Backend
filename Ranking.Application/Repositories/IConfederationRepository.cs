using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IConfederationRepository
    {
        Task<List<Confederation>> Get();
        Task<Confederation> Get(int id);
        Task Add(Confederation confederation);
        void Update(Confederation confederation);
        Task Delete(int id);
        Task SaveChanges();
    }
}
