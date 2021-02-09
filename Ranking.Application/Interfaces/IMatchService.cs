using Ranking.Domain;
using Ranking.Domain.Request;
using Ranking.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> Get();
        Task<List<Match>> Get(MatchCollectionRequest request);
        Task<List<Match>> GetByTournament(int id);
        Task<List<Match>> GetByTeam(int teamId);
        Task<List<Match>> GetByTeams(int team1Id, int team2Id, bool worldcup);
        Task<Match> Get(int id);
        Task Add(Match match);
        Task Update(Match match);
        Task<List<StreakCollectionResponse>> GetReportWinning();
        Task<List<StreakCollectionResponse>> GetReportUnbeaten();
        Task<List<StreakCollectionResponse>> GetReportLosing();
        Task<List<StreakCollectionResponse>> GetReportWinningless();
        Task<List<StreakCollectionResponse>> GetReportCleanSheets();
        Task<List<StreakCollectionResponse>> GetReportScoreless();
        Task<List<Match>> GetReportMargin();
        Task<List<Match>> GetReportGoals();
    }
}
