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
    public class MatchRepository : IMatchRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public MatchRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Match>> Get()
        {
            var matchesList = await _ctx.Matches
                                        .Include(e => e.Team1)
                                        .Include(e => e.Team2)
                                        .Include(e => e.Tournament)
                                        .ToListAsync();
            return _mapper.Map<List<Match>>(matchesList);
        }

        public async Task<List<Match>> GetByTournament(int tournamentId)
        {
            var matchesList = await _ctx.Matches
                                        .Include(e => e.Team1)
                                        .Include(e => e.Team2)
                                        .Where(e => e.TournamentID == tournamentId)
                                        .OrderBy(e => e.Date)
                                            .ThenBy(e => e.MatchRoundID)
                                            .ThenBy(e => e.Matchday)
                                            .ThenBy(e => e.Group)
                                        .ToListAsync();
            return _mapper.Map<List<Match>>(matchesList);
        }

        public async Task<List<Match>> GetByTeam(int teamId)
        {
            var matchesList = await _ctx.Matches
                                        .Include(e => e.Team1)
                                        .Include(e => e.Team2)
                                        .Include(e => e.Tournament)
                                        .Where(e => e.Team1ID == teamId || e.Team2ID == teamId)
                                        .OrderByDescending(e => e.Date)
                                            .ThenBy(e => e.MatchID)
                                        .ToListAsync();
            return _mapper.Map<List<Match>>(matchesList);
        }

        public async Task<Match> Get(int id)
        {
            var match = await _ctx.Matches.AsNoTracking()
                                        .Include(e => e.Team1)
                                        .Include(e => e.Team2)
                                        .Include(e => e.Tournament)
                                        .FirstOrDefaultAsync(e => e.MatchID == id);
            return _mapper.Map<Match>(match);
        }

        public async Task Add(Match match)
        {
            var matchToAdd = _mapper.Map<Matches>(match);
            await _ctx.Matches.AddAsync(matchToAdd);
        }

        public void Update(Match match)
        {
            var matchModified = _mapper.Map<Matches>(match);
            _ctx.Matches.Attach(matchModified);
            _ctx.Entry(matchModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var matchToDelete = await _ctx.Matches.FindAsync(id);
            _ctx.Matches.Remove(matchToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
