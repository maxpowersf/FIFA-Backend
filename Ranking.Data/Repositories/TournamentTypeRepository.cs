using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Data.Entities;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class TournamentTypeRepository : ITournamentTypeRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public TournamentTypeRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<TournamentType>> Get()
        {
            var tournamentTypeList = await _ctx.TournamentTypes.ToListAsync();
            return _mapper.Map<List<TournamentType>>(tournamentTypeList);
        }

        public async Task<TournamentType> Get(int id)
        {
            var tournamentType = await _ctx.TournamentTypes.FirstOrDefaultAsync(e => e.TournamentTypeID == id);
            return _mapper.Map<TournamentType>(tournamentType);
        }

        public async Task Add(TournamentType tournamentType)
        {
            var tournamentTypeToAdd = _mapper.Map<TournamentTypes>(tournamentType);
            await _ctx.AddAsync(tournamentTypeToAdd);
        }

        public void Update(TournamentType tournamentType)
        {
            var tournamentTypeModified = _mapper.Map<TournamentTypes>(tournamentType);
            _ctx.TournamentTypes.Attach(tournamentTypeModified);
            _ctx.Entry(tournamentTypeModified).State = EntityState.Modified;
        }

        public async Task Delete (int id)
        {
            var tournamentTypeToDelete = await _ctx.TournamentTypes.FirstOrDefaultAsync(e => e.TournamentTypeID == id);
            _ctx.TournamentTypes.Remove(tournamentTypeToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
