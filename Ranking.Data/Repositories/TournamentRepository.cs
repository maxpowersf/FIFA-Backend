﻿using AutoMapper;
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
                                        .Include(e => e.Positions)
                                        .OrderBy(e => e.Year)
                                        .ThenBy(e => e.TournamentType.Name)
                                        .ThenBy(e => e.Confederation.Name)
                                        .ToListAsync();
            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<List<Tournament>> GetByTeam(int teamId)
        {
            var tournamentList = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .Include(e => e.Positions)
                                        .Where(e => e.Positions.Any(f => f.TeamID == teamId))
                                        .OrderBy(e => e.TournamentType.FormatID)
                                            .ThenBy(e => e.Year)
                                        .ToListAsync();
            foreach(Tournaments tournament in tournamentList)
            {
                tournament.Positions = tournament.Positions.Where(e => e.TeamID == teamId).ToList();
            }
            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<Tournament> Get(int id)
        {
            var tournament = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .Include(e => e.Positions)
                                            .ThenInclude(e => e.Team)
                                            .ThenInclude(e => e.Confederation)
                                        .FirstOrDefaultAsync(e => e.TournamentID == id);
            tournament.Positions = tournament.Positions.OrderBy(x => x.Group)
                                                        .ThenBy(x => x.NoPosition)
                                                        .ThenByDescending(x => x.Qualified)
                                                        .ToList();
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
            var tournamentToDelete = await _ctx.Tournaments.FindAsync(id);
            _ctx.Tournaments.Remove(tournamentToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
