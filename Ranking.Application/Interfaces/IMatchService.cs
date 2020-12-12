using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> Get();
        Task<List<Match>> GetByTournament(int id);
        Task<List<Match>> GetByTeam(int teamId);
        Task<Match> Get(int id);
        Task Add(Match match);
        Task Update(Match match);
    }
}
