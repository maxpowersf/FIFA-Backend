using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Data.Entities;
using Ranking.Domain;
using Ranking.Domain.Enum;
using Ranking.Domain.Request;
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

        public async Task<List<Match>> Get(MatchCollectionRequest request)
        {
            var query = from match in _ctx.Matches
                        where (request.Team1ID.HasValue) ? (match.Team1ID == request.Team1ID || match.Team2ID == request.Team1ID) : true
                        where (request.Team2ID.HasValue) ? (match.Team2ID == request.Team2ID || match.Team1ID == request.Team2ID) : true
                        where (request.ConfederationID.HasValue) ? match.Team1.ConfederationID == request.ConfederationID || match.Team2.ConfederationID == request.ConfederationID : true
                        where (request.TournamentID.HasValue) ? match.TournamentID == request.TournamentID : true
                        where (request.TournamentTypeID.HasValue) ? match.Tournament.TournamentTypeID == request.TournamentTypeID : true
                        where (request.StartDate.HasValue) ? match.Date >= request.StartDate : true
                        where (request.EndDate.HasValue) ? match.Date <= request.EndDate : true
                        orderby match.Date ascending
                        select match;

            var matchesList = await query.Include(e => e.Team1)
                                            .Include(e => e.Team2)
                                            .Include(e => e.Tournament)
                                            .Take(request.Quantity.GetValueOrDefault(int.MaxValue))
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

        public async Task<List<Match>> GetByTeams(int team1Id, int team2Id, bool worldcup)
        {
            var matchesList = await _ctx.Matches
                                        .Include(e => e.Team1)
                                        .Include(e => e.Team2)
                                        .Include(e => e.Tournament)
                                        .Where(e => (e.Team1ID == team1Id || e.Team2ID == team1Id) && (e.Team1ID == team2Id || e.Team2ID == team2Id) &&
                                                        (!worldcup || e.Tournament.TournamentType.FormatID == (int)TournamentFormat.WorldCup))
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

        public async Task<List<Match>> GetReportMargin()
        {
            var query = from match in _ctx.Matches
                        let margin = Math.Abs(match.GoalsTeam1 - match.GoalsTeam2)
                        where margin >= 4
                        orderby margin descending, match.Date descending
                        select match;

            var matchesList = await query.Include(e => e.Team1)
                                            .Include(e => e.Team2)
                                            .Include(e => e.Tournament)
                                            .ToListAsync();

            return _mapper.Map<List<Match>>(matchesList);
        }

        public async Task<List<Match>> GetReportGoals()
        {
            var query = from match in _ctx.Matches
                        let goals = match.GoalsTeam1 + match.GoalsTeam2
                        where goals >= 6
                        orderby goals descending, match.Date descending
                        select match;

            var matchesList = await query.Include(e => e.Team1)
                                            .Include(e => e.Team2)
                                            .Include(e => e.Tournament)
                                            .ToListAsync();

            return _mapper.Map<List<Match>>(matchesList);
        }
    }
}
