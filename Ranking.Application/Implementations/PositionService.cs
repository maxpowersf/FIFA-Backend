using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Application.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            this._positionRepository = positionRepository;
        }
    }
}
