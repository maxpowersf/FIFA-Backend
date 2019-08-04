using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Data.Entities;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public TournamentRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Tournament>> Get()
        {
            var tournamentList = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .Include(e => e.Confederation)
                                        .OrderBy(e => e.Year)
                                        .ToListAsync();
            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<Tournament> Get(int id)
        {
            var tournament = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .FirstOrDefaultAsync(e => e.TournamentID == id);
            return _mapper.Map<Tournament>(tournament);
        }

        public async Task Add(Tournament tournament)
        {
            var tournamentToAdd = _mapper.Map<Tournaments>(tournament);
            await _ctx.Tournaments.AddAsync(tournamentToAdd);
        }

        public void Update(Tournament tournament)
        {
            var tournamentModified = _mapper.Map<Tournaments>(tournament);
            _ctx.Tournaments.Attach(tournamentModified);
            _ctx.Entry(tournamentModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var tournamentToDelete = await _ctx.Tournaments.FirstOrDefaultAsync(e => e.TournamentID == id);
            _ctx.Tournaments.Remove(tournamentToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
