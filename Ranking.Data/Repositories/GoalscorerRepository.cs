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
    public class GoalscorerRepository : IGoalscorerRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public GoalscorerRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Goalscorer>> Get()
        {
            var goalscorers = await _ctx.Goalscorers
                                    .Include(e => e.Tournament)
                                    .Include(e => e.Player)
                                    .ToListAsync();
            return _mapper.Map<List<Goalscorer>>(goalscorers);
        }

        public async Task<List<Goalscorer>> GetByTournament(int tournamentId)
        {
            var goalscorers = await _ctx.Goalscorers
                                        .Include(e => e.Tournament)
                                        .Include(e => e.Player)
                                            .ThenInclude(f => f.Team)
                                        .Where(e => e.TournamentID == tournamentId)
                                        .OrderByDescending(e => e.Goals)
                                        .ToListAsync();
            return _mapper.Map<List<Goalscorer>>(goalscorers);
        }

        public async Task<Goalscorer> Get(int id)
        {
            var goalscorer = await _ctx.Goalscorers.FirstOrDefaultAsync(e => e.GoalscorerID == id);
            return _mapper.Map<Goalscorer>(goalscorer);
        }

        public async Task Add(Goalscorer goalscorer)
        {
            var goalscorerToAdd = _mapper.Map<Goalscorers>(goalscorer);
            await _ctx.Goalscorers.AddAsync(goalscorerToAdd);
        }

        public void Update(Goalscorer goalscorer)
        {
            var goalscorerModified = _mapper.Map<Goalscorers>(goalscorer);
            _ctx.Goalscorers.Attach(goalscorerModified);
            _ctx.Entry(goalscorerModified).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var goalscorerToDelete = await _ctx.Goalscorers.FindAsync(id);
            _ctx.Goalscorers.Remove(goalscorerToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
