using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITournamentTypeRepository _tournamentTypeRepository;

        public PlayerService(IPlayerRepository playerRepository, ITournamentTypeRepository tournamentTypeRepository)
        {
            this._playerRepository = playerRepository;
            this._tournamentTypeRepository = tournamentTypeRepository;
        }

        public Task<List<Player>> Get()
        {
            return _playerRepository.Get();
        }

        public Task<List<Player>> GetByTeam(int teamId)
        {
            return _playerRepository.GetByTeam(teamId);
        }

        public async Task<List<Player>> GetPlayersWithGoals(int tournamenttypeID)
        {
            List<Player> playerList;
            TournamentType tournamentType = await _tournamentTypeRepository.Get(tournamenttypeID);
            switch (tournamentType.Format)
            {
                case TournamentFormat.WorldCup:
                    playerList = await _playerRepository.GetWorldCupGoals();
                    break;
                case TournamentFormat.ConfederationsCup:
                    playerList = await _playerRepository.GetConfederationsCupGoals();
                    break;
                case TournamentFormat.ConfederationTournament:
                    playerList = await _playerRepository.GetConfederationTournamentGoals(tournamentType.ConfederationID);
                    break;
                case TournamentFormat.Qualification:
                    playerList = await _playerRepository.GetQualificationGoals();
                    break;
                default:
                    playerList = await _playerRepository.Get();
                    break;
            }

            return playerList;
        }

        public Task<Player> Get(int id)
        {
            return _playerRepository.Get(id);
        }

        public Task<Player> Get(string name, string teamName)
        {
            return _playerRepository.Get(name, teamName);
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
