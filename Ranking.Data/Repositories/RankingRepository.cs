using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public RankingRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<Domain.Ranking> GetActual(int teamId)
        {
            var ranking = await _ctx.Rankings.AsNoTracking().LastOrDefaultAsync(e => e.TeamID == teamId);
            return _mapper.Map<Domain.Ranking>(ranking);
        }

        public void Update(Domain.Ranking ranking)
        {
            Entities.Rankings rankingUpdated = _mapper.Map<Entities.Rankings>(ranking);
            _ctx.Rankings.Attach(rankingUpdated);
            _ctx.Entry(rankingUpdated).State = EntityState.Modified;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
