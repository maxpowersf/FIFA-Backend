using AutoMapper;
using Ranking.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
