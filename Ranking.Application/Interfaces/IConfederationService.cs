using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IConfederationService
    {
        Task<List<Confederation>> Get();
        Task<Confederation> Get(int id);
        Task Add(Confederation confederation);
        Task Update(Confederation confederation);
        Task Delete(int id);
    }
}
