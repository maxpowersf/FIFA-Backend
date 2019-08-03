using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface ITournamentTypeService
    {
        Task<List<TournamentType>> Get();
        Task<TournamentType> Get(int id);
        Task Add(TournamentType tournamentType);
        Task Update(TournamentType tournamentType);
        Task Delete(int id);
    }
}
