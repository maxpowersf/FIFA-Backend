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
    public class H2HWorldCupRepository : IH2HWorldCupRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public H2HWorldCupRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Head2HeadWorldCup>> Get()
        {
            var h2hList = await _ctx.H2HWorldCup
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                    .ToListAsync();
            return _mapper.Map<List<Head2HeadWorldCup>>(h2hList);
        }

        public async Task<List<Head2HeadWorldCup>> GetByTeam(int teamId)
        {
            var h2hList = await _ctx.H2HWorldCup
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                        .ThenInclude(e => e.Confederation)
                                    .Where(e => e.Team1ID == teamId)
                                    .OrderBy(e => e.Team2.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Head2HeadWorldCup>>(h2hList);
        }

        public async Task<Head2HeadWorldCup> GetByTeams(int team1Id, int team2Id)
        {
            var h2h = await _ctx.H2HWorldCup.AsNoTracking()
                                            .Include(e => e.Team1)
                                            .Include(e => e.Team2)
                                                .ThenInclude(e => e.Confederation)
                                            .FirstOrDefaultAsync(e => e.Team1ID == team1Id && e.Team2ID == team2Id);
            return _mapper.Map<Head2HeadWorldCup>(h2h);
        }

        public async Task<Head2HeadWorldCup> Get(int id)
        {
            var h2h = await _ctx.H2HWorldCup.AsNoTracking()
                                            .Include(e => e.Team1)
                                            .Include(e => e.Team2)
                                            .FirstOrDefaultAsync(e => e.H2HWorldCupID == id);
            return _mapper.Map<Head2HeadWorldCup>(h2h);
        }

        public Head2HeadWorldCup GetOrCreateByTeams(int team1Id, int team2Id)
        {
            var h2h = _ctx.H2HWorldCup.AsNoTracking()
                                        .FirstOrDefault(e => e.Team1ID == team1Id && e.Team2ID == team2Id);

            if (h2h == null)
            {
                var h2hToAdd = new H2HWorldCup
                {
                    Team1ID = team1Id,
                    Team2ID = team2Id
                };

                var createdH2H = _ctx.H2HWorldCup.Add(h2hToAdd);
                _ctx.SaveChanges();
                _ctx.Entry(h2hToAdd).State = EntityState.Detached;

                h2h = _ctx.H2HWorldCup.AsNoTracking()
                                        .FirstOrDefault(e => e.Team1ID == team1Id && e.Team2ID == team2Id);
            }

            return _mapper.Map<Head2HeadWorldCup>(h2h);
        }

        public async Task Add(Head2HeadWorldCup h2h)
        {
            var h2hToAdd = _mapper.Map<H2HWorldCup>(h2h);
            await _ctx.H2HWorldCup.AddAsync(h2hToAdd);
        }

        public void Update(Head2HeadWorldCup h2h)
        {
            var h2hModified = _mapper.Map<H2HWorldCup>(h2h);
            _ctx.H2HWorldCup.Attach(h2hModified);
            _ctx.Entry(h2hModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var h2hToDelete = await _ctx.H2HWorldCup.FindAsync(id);
            _ctx.H2HWorldCup.Remove(h2hToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
