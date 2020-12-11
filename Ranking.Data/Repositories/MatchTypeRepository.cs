using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class MatchTypeRepository : IMatchTypeRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public MatchTypeRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<MatchType>> Get()
        {
            var matchTypeList = await _ctx.MatchTypes.ToListAsync();
            return _mapper.Map<List<MatchType>>(matchTypeList);
        }

        public async Task<MatchType> Get(int id)
        {
            var matchType = await _ctx.MatchTypes.AsNoTracking().FirstOrDefaultAsync(c => c.MatchTypeID == id);
            return _mapper.Map<MatchType>(matchType);
        }

        public async Task Add(MatchType matchType)
        {
            var matchTypeToAdd = _mapper.Map<Entities.MatchTypes>(matchType);
            await _ctx.AddAsync(matchTypeToAdd);
        }

        public void Update(MatchType matchType)
        {
            Entities.MatchTypes matchTypeUpdated = _mapper.Map<Entities.MatchTypes>(matchType);
            _ctx.MatchTypes.Attach(matchTypeUpdated);
            _ctx.Entry(matchTypeUpdated).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var matchTypeToDelete = await _ctx.MatchTypes.FindAsync(id);
            _ctx.MatchTypes.Remove(matchTypeToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
