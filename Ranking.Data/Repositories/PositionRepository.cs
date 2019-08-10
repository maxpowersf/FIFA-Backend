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
    public class PositionRepository : IPositionRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public PositionRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Position>> Get()
        {
            var positions = await _ctx.Positions
                                    .Include(e => e.Tournament)
                                    .Include(e => e.Team)
                                    .ToListAsync();
            return _mapper.Map<List<Position>>(positions);
        }

        public async Task<List<Position>> GetByTeam(int teamId)
        {
            var positions = await _ctx.Positions
                                        .Include(e => e.Tournament)
                                        .Include(e => e.Team)
                                        .Where(e => e.TeamID == teamId)
                                        .ToListAsync();
            return _mapper.Map<List<Position>>(positions);
        }

        public async Task<List<Position>> GetByTournament(int tournamentId)
        {
            var positions = await _ctx.Positions
                                        .Include(e => e.Tournament)
                                        .Include(e => e.Team)
                                        .Where(e => e.TournamentID == tournamentId)
                                        .ToListAsync();
            return _mapper.Map<List<Position>>(positions);
        }

        public async Task<Position> Get(int id)
        {
            var position = await _ctx.Positions.FirstOrDefaultAsync(e => e.PositionID == id);
            return _mapper.Map<Position>(position);
        }

        public async Task Add(Position position)
        {
            var positionToAdd = _mapper.Map<Positions>(position);
            await _ctx.Positions.AddAsync(positionToAdd);
        }

        public void Update(Position position)
        {
            var positionModified = _mapper.Map<Positions>(position);
            _ctx.Positions.Attach(positionModified);
            _ctx.Entry(positionModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var positionToDelete = await _ctx.Positions.FindAsync(id);
            _ctx.Positions.Remove(positionToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
