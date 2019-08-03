using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITournamentTypeRepository
    {
        Task<List<TournamentType>> Get();
        Task<TournamentType> Get(int id);
        Task Add(TournamentType tournamentType);
        void Update(TournamentType tournamentType);
        Task Delete(int id);
        Task SaveChanges();
    }
}
