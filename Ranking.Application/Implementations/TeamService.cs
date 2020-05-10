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
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITournamentTypeRepository _tournamentTypeRepository;

        public TeamService(ITeamRepository teamRepository, ITournamentTypeRepository tournamentTypeRepository)
        {
            this._teamRepository = teamRepository;
            this._tournamentTypeRepository = tournamentTypeRepository;
        }
        public Task<List<Team>> Get()
        {
            return _teamRepository.Get();
        }

        public async Task<List<Team>> GetAllTeamsByConfederation(int confederationID)
        {
            return await _teamRepository.GetAllByConfederation(confederationID);
        }

        public async Task<List<Team>> GetFirstTeams(int quantity)
        {
            return await _teamRepository.GetFirstTeams(quantity);
        }

        public async Task<List<Team>> GetTeamsWithTitles(int tournamenttypeID, int quantity)
        {
            List<Team> teamList;
            if (tournamenttypeID > 0)
            {
                TournamentType tournamentType = await _tournamentTypeRepository.Get(tournamenttypeID);
                switch (tournamentType.Format)
                {
                    case TournamentFormat.WorldCup:
                        teamList = await _teamRepository.GetWorldCupTitles();
                        break;
                    case TournamentFormat.ConfederationsCup:
                        teamList = await _teamRepository.GetConfederationsCupTitles();
                        break;
                    case TournamentFormat.ConfederationTournament:
                        teamList = await _teamRepository.GetConfederationTournamentTitles(tournamentType.ConfederationID);
                        break;
                    case TournamentFormat.Qualification:
                        teamList = await _teamRepository.GetWorldCupQualifications();
                        break;
                    default:
                        teamList = await _teamRepository.Get();
                        break;
                }

                return teamList;
            }
            else
            {
                teamList = await _teamRepository.GetTeamsWithTitles(quantity);
                return teamList;
            }
        }

        public Task<Team> Get(int id)
        {
            return _teamRepository.Get(id);
        }

        public async Task Add(Team team)
        {
            await _teamRepository.Add(team);
            await _teamRepository.SaveChanges();
        }

        public async Task Update(Team team)
        {
            _teamRepository.Update(team);
            await _teamRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _teamRepository.Delete(id);
            await _teamRepository.SaveChanges();
        }

        public async Task UpdateRankings()
        {
            var teamsOrdered = _teamRepository.GetOrdered();

            foreach(Team team in teamsOrdered)
            {
                int newRank = teamsOrdered.IndexOf(team) + 1;
                int newLowestRank = (newRank > team.LowestRank) ? newRank : team.LowestRank;
                int newHighestRank = (newRank < team.HighestRank) ? newRank : team.HighestRank;
                team.ActualRank = newRank;
                team.LowestRank = newLowestRank;
                team.HighestRank = newHighestRank;

                _teamRepository.Update(team);
            }

            await _teamRepository.SaveChanges();
        }
    }
}
