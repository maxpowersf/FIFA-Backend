using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            this._matchRepository = matchRepository;
        }

        public async Task Add(Match match)
        {
            await _matchRepository.Add(match);
            await _matchRepository.SaveChanges();
        }

        public Task<List<Match>> Get()
        {
            return _matchRepository.Get();
        }

        public Task<Match> Get(int id)
        {
            return _matchRepository.Get(id);
        }

        public Task<List<Match>> GetByTeam(int teamId)
        {
            return _matchRepository.GetByTeam(teamId);
        }

        public async void Update(Match match)
        {
            _matchRepository.Update(match);
            await _matchRepository.SaveChanges();
        }
    }
}
