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
        Task<List<Team>> GetAllTeamsByConfederation(int confederationID);
        Task<Confederation> Get(int id);
        Task Add(Confederation confederation);
        Task Update(Confederation confederation);
        Task Delete(int id);
    }
}
