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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public PlayerRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Player>> Get()
        {
            var playersList = await _ctx.Players
                                        .Include(e => e.Team)
                                        .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<List<Player>> GetByTeam(int teamId)
        {
            var playersList = await _ctx.Players
                                        .Include(e => e.Team)
                                        .Where(e => e.TeamID == teamId)
                                        .OrderBy(e => e.PositionID)
                                            .ThenBy(e => e.Name)
                                        .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<List<Player>> GetWorldCupGoals()
        {
            var playersList = await _ctx.Players
                                    .Include(e => e.Team)
                                    .Where(e => e.WorldCupGoals > 0)
                                    .OrderByDescending(e => e.WorldCupGoals)
                                        .ThenBy(e => e.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<List<Player>> GetConfederationsCupGoals()
        {
            var playersList = await _ctx.Players
                                    .Include(e => e.Team)
                                    .Where(e => e.ConfederationsGoals > 0)
                                    .OrderByDescending(e => e.ConfederationsGoals)
                                        .ThenBy(e => e.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<List<Player>> GetConfederationTournamentGoals(int confederationID)
        {
            var playersList = await _ctx.Players
                                    .Include(e => e.Team)
                                    .Where(e => e.ConfederationTournamentGoals > 0 && e.Team.ConfederationID == confederationID)
                                    .OrderByDescending(e => e.ConfederationTournamentGoals)
                                        .ThenBy(e => e.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<List<Player>> GetQualificationGoals()
        {
            var playersList = await _ctx.Players
                                    .Include(e => e.Team)
                                    .Where(e => e.QualificationGoals > 0)
                                    .OrderByDescending(e => e.QualificationGoals)
                                        .ThenBy(e => e.Name)
                                    .ToListAsync();
            return _mapper.Map<List<Player>>(playersList);
        }

        public async Task<Player> Get(int id)
        {
            var player = await _ctx.Players
                                        .Include(e => e.Team)
                                        .FirstOrDefaultAsync(e => e.PlayerID == id);
            return _mapper.Map<Player>(player);
        }

        public async Task Add(Player player)
        {
            var playerToAdd = _mapper.Map<Players>(player);
            await _ctx.Players.AddAsync(playerToAdd);
        }

        public void Update(Player player)
        {
            var playerModified = _mapper.Map<Players>(player);
            _ctx.Players.Attach(playerModified);
            _ctx.Entry(playerModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var playerToDelete = await _ctx.Players.FindAsync(id);
            _ctx.Players.Remove(playerToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
