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
    public class TeamStatRepository : ITeamStatRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public TeamStatRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<TeamStat>> Get()
        {
            var teamStatsList = await _ctx.TeamStats
                                        .Include(e => e.Team)
                                        .OrderBy(e => e.Points)
                                        .ToListAsync();
            return _mapper.Map<List<TeamStat>>(teamStatsList);
        }

        public async Task<List<TeamStat>> GetByTeam(int teamId)
        {
            var teamStatsList = await _ctx.TeamStats
                                        .Include(e => e.Team)
                                        .Where(e => e.TeamID == teamId)
                                        .ToListAsync();
            return _mapper.Map<List<TeamStat>>(teamStatsList);
        }

        public TeamStat GetOrCreateByTeam(int teamId)
        {
            var teamStat = _ctx.TeamStats.AsNoTracking()
                                        .FirstOrDefault(e => e.TeamID == teamId);

            if(teamStat == null)
            {
                var teamStatToAdd = new TeamStats
                {
                    TeamID = teamId
                };

                var createdTeamStat = _ctx.TeamStats.Add(teamStatToAdd);
                _ctx.SaveChanges();
                _ctx.Entry(teamStatToAdd).State = EntityState.Detached;

                teamStat = _ctx.TeamStats.AsNoTracking()
                                        .FirstOrDefault(e => e.TeamID == teamId);
            }

            return _mapper.Map<TeamStat>(teamStat);
        }

        public async Task<TeamStat> Get(int id)
        {
            var teamStat = await _ctx.TeamStats
                                        .Include(e => e.Team)
                                        .FirstOrDefaultAsync(e => e.TeamStatsID == id);
            return _mapper.Map<TeamStat>(teamStat);
        }

        public async Task Add(TeamStat teamStat)
        {
            var teamStatToAdd = _mapper.Map<TeamStats>(teamStat);
            await _ctx.TeamStats.AddAsync(teamStatToAdd);
        }

        public void Update(TeamStat teamStat)
        {
            var teamStatModified = _mapper.Map<TeamStats>(teamStat);
            _ctx.TeamStats.Attach(teamStatModified);
            _ctx.Entry(teamStatModified).State = EntityState.Modified;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
