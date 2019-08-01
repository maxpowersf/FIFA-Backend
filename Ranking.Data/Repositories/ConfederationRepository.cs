using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Data.Repositories
{
    public class ConfederationRepository : IConfederationRepository
    {
        private readonly RankingContext _ctx;
        private readonly IMapper _mapper;

        public ConfederationRepository(RankingContext ctx, IMapper mapper)
        {
            this._ctx = ctx;
            this._mapper = mapper;
        }

        public async Task<List<Confederation>> Get()
        {
            var confederationList = await _ctx.Confederations
                                                .ToListAsync();
            return _mapper.Map<List<Confederation>>(confederationList);
        }
        
        public async Task<Confederation> Get(int id)
        {
            var confederation = await _ctx.Confederations
                                            .Include(e => e.Teams)
                                            .FirstOrDefaultAsync(c => c.ConfederationID == id);
            return _mapper.Map<Confederation>(confederation);
        }

        public async Task Add(Confederation confederation)
        {
            var confederationToAdd = _mapper.Map<Entities.Confederations>(confederation);
            await _ctx.AddAsync(confederationToAdd);
        }

        public void Update(Confederation confederation)
        {
            Entities.Confederations confederationUpdated = _mapper.Map<Entities.Confederations>(confederation);
            _ctx.Confederations.Attach(confederationUpdated);
            _ctx.Entry(confederationUpdated).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var confederationToDelete = await _ctx.Confederations.FindAsync(id);
            _ctx.Confederations.Remove(confederationToDelete);
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
