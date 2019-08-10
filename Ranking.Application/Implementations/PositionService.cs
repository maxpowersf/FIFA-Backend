using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            this._positionRepository = positionRepository;
        }

        public async Task<List<Position>> Get()
        {
            return await _positionRepository.Get();
        }

        public async Task<List<Position>> GetByTeam(int id)
        {
            return await _positionRepository.GetByTeam(id);
        }

        public async Task<List<Position>> GetByTournament(int id)
        {
            return await _positionRepository.GetByTournament(id);
        }

        public async Task<Position> Get(int id)
        {
            return await _positionRepository.Get(id);
        }

        public async Task Add(Position position)
        {
            await _positionRepository.Add(position);
            await _positionRepository.SaveChanges();
        }

        public async Task Update(Position position)
        {
            _positionRepository.Update(position);
            await _positionRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _positionRepository.Delete(id);
            await _positionRepository.SaveChanges();
        }
    }
}
