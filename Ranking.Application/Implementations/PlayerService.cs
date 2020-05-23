using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            this._playerRepository = playerRepository;
        }

        public Task<List<Player>> Get()
        {
            return _playerRepository.Get();
        }

        public Task<List<Player>> GetByTeam(int teamId)
        {
            return _playerRepository.GetByTeam(teamId);
        }

        public Task<Player> Get(int id)
        {
            return _playerRepository.Get(id);
        }

        public async Task Add(Player player)
        {
            await _playerRepository.Add(player);
            await _playerRepository.SaveChanges();
        }

        public async Task Update(Player player)
        {
            _playerRepository.Update(player);
            await _playerRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _playerRepository.Delete(id);
            await _playerRepository.SaveChanges();
        }
    }
}
