using Ranking.Domain;
using Ranking.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface IMatchRepository
    {
        Task<List<Match>> Get();
        Task<List<Match>> Get(MatchCollectionRequest request);
        Task<List<Match>> GetByTournament(int id);
        Task<List<Match>> GetByTeam(int teamId);
        Task<List<Match>> GetByTeams(int team1Id, int team2Id, bool worldcup);
        Task<Match> Get(int id);
        Task Add(Match match);
        void Update(Match match);
        Task Delete(int id);
        Task SaveChanges();
        Task<List<Match>> GetReportMargin();
        Task<List<Match>> GetReportGoals();
    }
}
