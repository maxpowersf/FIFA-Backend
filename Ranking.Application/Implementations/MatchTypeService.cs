using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class MatchTypeService : IMatchTypeService
    {
        private readonly IMatchTypeRepository _matchTypeRepository;

        public MatchTypeService(IMatchTypeRepository matchTypeRepository)
        {
            this._matchTypeRepository = matchTypeRepository;
        }

        public Task<List<MatchType>> Get()
        {
            return _matchTypeRepository.Get();
        }

        public Task<MatchType> Get(int id)
        {
            return _matchTypeRepository.Get(id);
        }

        public async Task Add(MatchType matchType)
        {
            await _matchTypeRepository.Add(matchType);
            await _matchTypeRepository.SaveChanges();
        }

        public async Task Update(MatchType matchType)
        {
            _matchTypeRepository.Update(matchType);
            await _matchTypeRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _matchTypeRepository.Delete(id);
            await _matchTypeRepository.SaveChanges();
        }
    }
}
