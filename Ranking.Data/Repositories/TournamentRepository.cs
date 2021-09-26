using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Data.Entities;
using Ranking.Domain;
using Ranking.Domain.Enum;
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
                                        .OrderBy(e => e.Year)
                                        .ThenBy(e => e.TournamentType.Name)
                                        .ThenBy(e => e.Confederation.Name)
                                        .ToListAsync();
            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<List<Tournament>> GetByTeam(int teamId, int confederationId)
        {
            var tournamentList = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .Include(e => e.Positions)
                                        .Where(e => e.Positions.Any(f => f.TeamID == teamId) || 
                                                e.TournamentType.FormatID == (int)TournamentFormat.WorldCup ||
                                                e.TournamentType.FormatID == (int)TournamentFormat.ConfederationsCup ||
                                                e.ConfederationID == confederationId)
                                        .OrderBy(e => e.TournamentType.FormatID)
                                            .ThenBy(e => e.Year)
                                        .ToListAsync();

            foreach (Tournaments tournament in tournamentList)
            {
                tournament.Positions = tournament.Positions.Where(e => e.TeamID == teamId).ToList();
            }

            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<List<Tournament>> GetByTournamentTypeWithPositions(int tournamentTypeId)
        {
            var tournamentList = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .Include(e => e.Positions)
                                            .ThenInclude(e => e.Team)
                                        .Where(e => e.TournamentTypeID == tournamentTypeId && e.Positions.Count > 0)
                                        .OrderBy(e => e.Year)
                                        .ToListAsync();

            foreach (Tournaments tournament in tournamentList)
            {
                tournament.Positions = tournament.Positions.OrderBy(e => e.NoPosition).Take(4).ToList();
            }

            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<List<Tournament>> GetByTournamentTypeAndConfederation(int tournamentTypeId, int confederationId)
        {
            var tournamentList = await _ctx.Tournaments
                                            .Include(e => e.TournamentType)
                                            .Include(e => e.Confederation)
                                            .Where(e => e.TournamentTypeID == tournamentTypeId)
                                            .OrderBy(e => e.Year)
                                            .ToListAsync();

            if(confederationId > 0)
            {
                if (confederationId == 3 || confederationId == 4)
                {
                    tournamentList = tournamentList.Where(e => e.ConfederationID == confederationId || e.ConfederationID == null).ToList();
                }
                else
                {
                    tournamentList = tournamentList.Where(e => e.ConfederationID == confederationId).ToList();
                }
            }

            return _mapper.Map<List<Tournament>>(tournamentList);
        }

        public async Task<Tournament> Get(int id)
        {
            var tournament = await _ctx.Tournaments.AsNoTracking()
                                        .Include(e => e.TournamentType)
                                            .ThenInclude(e => e.MatchType)
                                        .Include(e => e.Positions)
                                            .ThenInclude(e => e.Team)
                                            .ThenInclude(e => e.Confederation)
                                        .FirstOrDefaultAsync(e => e.TournamentID == id);

            if (tournament != null)
            {
                tournament.Positions = tournament.Positions.OrderBy(x => x.Group)
                                                            .ThenBy(x => x.NoPosition)
                                                            .ThenByDescending(x => x.Qualified)
                                                            .ThenByDescending(x => x.Round)
                                                            .ToList();
            }

            return _mapper.Map<Tournament>(tournament);
        }
        public async Task<Tournament> GetByQualificationYear(int year)
        {
            var tournament = await _ctx.Tournaments
                                        .Include(e => e.TournamentType)
                                        .FirstOrDefaultAsync(e => e.Year == year && e.TournamentType.FormatID == (int)TournamentFormat.WorldCup);

            return _mapper.Map<Tournament>(tournament);
        }

        public async Task<Tournament> GetWithoutPositions(int id)
        {
            var tournament = await _ctx.Tournaments.AsNoTracking()
                                        .Include(e => e.TournamentType)
                                            .ThenInclude(e => e.MatchType)
                                        .FirstOrDefaultAsync(e => e.TournamentID == id);

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
