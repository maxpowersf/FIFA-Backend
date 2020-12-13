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
    public class TeamStatWorldCupRepository : ITeamStatWorldCupRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public TeamStatWorldCupRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<TeamStatWorldCup>> Get()
        {
            var teamStatsWorldCupList = await _ctx.TeamStatsWorldCup
                                        .Include(e => e.Team)
                                            .ThenInclude(e => e.Confederation)
                                        .OrderByDescending(e => e.Points)
                                            .ThenByDescending(e => e.GoalDifference)
                                            .ThenByDescending(e => e.GoalsFavor)
                                        .ToListAsync();
            return _mapper.Map<List<TeamStatWorldCup>>(teamStatsWorldCupList);
        }

        public async Task<List<TeamStatWorldCup>> GetByTeam(int teamId)
        {
            var teamStatsWorldCupList = await _ctx.TeamStatsWorldCup
                                        .Include(e => e.Team)
                                        .Where(e => e.TeamID == teamId)
                                        .ToListAsync();
            return _mapper.Map<List<TeamStatWorldCup>>(teamStatsWorldCupList);
        }

        public TeamStatWorldCup GetOrCreateByTeam(int teamId)
        {
            var teamStatWorldCup = _ctx.TeamStatsWorldCup.AsNoTracking()
                                        .FirstOrDefault(e => e.TeamID == teamId);

            if (teamStatWorldCup == null)
            {
                var teamStatWorldCupToAdd = new TeamStatsWorldCup
                {
                    TeamID = teamId
                };

                var createdTeamStat = _ctx.TeamStatsWorldCup.Add(teamStatWorldCupToAdd);
                _ctx.SaveChanges();
                _ctx.Entry(teamStatWorldCupToAdd).State = EntityState.Detached;

                teamStatWorldCup = _ctx.TeamStatsWorldCup
                                        .AsNoTracking()
                                        .FirstOrDefault(e => e.TeamID == teamId);
            }

            return _mapper.Map<TeamStatWorldCup>(teamStatWorldCup);
        }

        public async Task<TeamStatWorldCup> Get(int id)
        {
            var teamStatWorldCup = await _ctx.TeamStatsWorldCup
                                        .Include(e => e.Team)
                                        .FirstOrDefaultAsync(e => e.TeamStatsWorldCupID == id);
            return _mapper.Map<TeamStatWorldCup>(teamStatWorldCup);
        }

        public async Task Add(TeamStatWorldCup teamStatWorldCup)
        {
            var teamStatWorldCupToAdd = _mapper.Map<TeamStatsWorldCup>(teamStatWorldCup);
            await _ctx.TeamStatsWorldCup.AddAsync(teamStatWorldCupToAdd);
        }

        public void Update(TeamStatWorldCup teamStatWorldCup)
        {
            var teamStatWorldCupModified = _mapper.Map<TeamStatsWorldCup>(teamStatWorldCup);
            _ctx.TeamStatsWorldCup.Attach(teamStatWorldCupModified);
            _ctx.Entry(teamStatWorldCupModified).State = EntityState.Modified;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
