using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public TeamRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Team>> Get()
        {
            var teamList = await _ctx.Teams.AsNoTracking()
                                        .Include(e => e.Confederation)
                                        .Include(e => e.Rankings)
                                        .ToListAsync();
            return _mapper.Map<List<Team>>(teamList);
        }

        public List<Team> GetOrdered()
        {
            var teamList = _ctx.Teams.AsNoTracking()
                                .Include(e => e.Confederation)
                                .OrderByDescending(e => e.TotalPoints)
                                .ToList();
            return _mapper.Map<List<Team>>(teamList);
        }

        public async Task<List<Team>> GetAllByConfederation(int confederationID)
        {
            var teamList = await _ctx.Teams.AsNoTracking()
                                        .Include(e => e.Confederation)
                                        .Include(e => e.Rankings)
                                        .Where(e => e.ConfederationID == confederationID)
                                        .ToListAsync();
            return _mapper.Map<List<Team>>(teamList);
        }

        public async Task<Team> Get(int id)
        {
            var team = await _ctx.Teams.AsNoTracking()
                                    .Include(e => e.Confederation)
                                    .FirstOrDefaultAsync(c => c.TeamID == id);
            return _mapper.Map<Team>(team);
        }

        public async Task Add(Team team)
        {
            var teamToAdd = _mapper.Map<Entities.Teams>(team);
            await _ctx.AddAsync(teamToAdd);
        }

        public void Update(Team team)
        {
            Entities.Teams teamUpdated = _mapper.Map<Entities.Teams>(team);
            _ctx.Teams.Attach(teamUpdated);
            _ctx.Entry(teamUpdated).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var teamToDelete = await _ctx.Teams.FindAsync(id);
            _ctx.Teams.Remove(teamToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
