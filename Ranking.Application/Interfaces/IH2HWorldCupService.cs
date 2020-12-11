using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Interfaces
{
    public interface IH2HWorldCupService
    {
        Task<List<Head2HeadWorldCup>> Get();
        Task<List<Head2HeadWorldCup>> GetByTeam(int teamId);
        Task<Head2HeadWorldCup> GetByTeams(int team1Id, int team2Id);
        Task<Head2HeadWorldCup> Get(int id);
        Task Add(Head2HeadWorldCup h2hWorldCup);
        void Update(Head2HeadWorldCup h2hWorldCup);
        Task Delete(int id);
    }
}
