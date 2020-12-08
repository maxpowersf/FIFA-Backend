using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IRankingService
    {
        Task AddMatchRanking(MatchRanking match);
        Task AddMatch(Match match);
        Task FinishPeriod();
    }
}
