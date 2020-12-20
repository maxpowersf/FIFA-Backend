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
    public class H2HRepository : IH2HRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public H2HRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Head2Head>> Get()
        {
            var h2hList = await _ctx.H2H
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                    .ToListAsync();
            return _mapper.Map<List<Head2Head>>(h2hList);
        }

        public async Task<List<Head2Head>> GetByTeam(int teamId)
        {
            var h2hList = await _ctx.H2H
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                        .ThenInclude(e => e.Confederation)
                                    .Where(e => e.Team1ID == teamId)
                                    .OrderBy(e => e.Team2.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Head2Head>>(h2hList);
        }

        public async Task<Head2Head> GetByTeams(int team1Id, int team2Id)
        {
            var h2h = await _ctx.H2H.AsNoTracking()
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                        .ThenInclude(e => e.Confederation)
                                    .FirstOrDefaultAsync(e => e.Team1ID == team1Id && e.Team2ID == team2Id);
            return _mapper.Map<Head2Head>(h2h);
        }

        public async Task<Head2Head> Get(int id)
        {
            var h2h = await _ctx.H2H.AsNoTracking()
                                    .Include(e => e.Team1)
                                    .Include(e => e.Team2)
                                    .FirstOrDefaultAsync(e => e.H2HID == id);
            return _mapper.Map<Head2Head>(h2h);
        }

        public Head2Head GetOrCreateByTeams(int team1Id, int team2Id)
        {
            var h2h = _ctx.H2H.AsNoTracking()
                                .FirstOrDefault(e => e.Team1ID == team1Id && e.Team2ID == team2Id);

            if (h2h == null)
            {
                var h2hToAdd = new H2H
                {
                    Team1ID = team1Id,
                    Team2ID = team2Id
                };

                var createdH2H = _ctx.H2H.Add(h2hToAdd);
                _ctx.SaveChanges();
                _ctx.Entry(h2hToAdd).State = EntityState.Detached;

                h2h = _ctx.H2H.AsNoTracking()
                                .FirstOrDefault(e => e.Team1ID == team1Id && e.Team2ID == team2Id);
            }

            return _mapper.Map<Head2Head>(h2h);
        }

        public async Task Add(Head2Head h2h)
        {
            var h2hToAdd = _mapper.Map<H2H>(h2h);
            await _ctx.H2H.AddAsync(h2hToAdd);
        }

        public void Update(Head2Head h2h)
        {
            var h2hModified = _mapper.Map<H2H>(h2h);
            _ctx.H2H.Attach(h2hModified);
            _ctx.Entry(h2hModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var h2hToDelete = await _ctx.H2H.FindAsync(id);
            _ctx.H2H.Remove(h2hToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
