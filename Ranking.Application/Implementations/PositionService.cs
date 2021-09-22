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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITeamRepository _teamRepository;

        public PositionService(IPositionRepository positionRepository, ITournamentRepository tournamentRepository, ITeamRepository teamRepository)
        {
            this._positionRepository = positionRepository;
            this._tournamentRepository = tournamentRepository;
            this._teamRepository = teamRepository;
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

        public async Task Add(List<Position> positions)
        {
            Tournament tournament = await _tournamentRepository.Get(positions[0].TournamentID);
            foreach(Position position in positions)
            {
                await _positionRepository.Add(position);

                if(position.Qualified || position.NoPosition == 1)
                {
                    Team team = await _teamRepository.Get(position.TeamID);
                    switch (tournament.TournamentType.Format)
                    {
                        case TournamentFormat.Qualification:
                            team.WorldCupQualifications++;
                            break;
                        case TournamentFormat.WorldCup:
                            team.WorldCupTitles++;
                            break;
                        case TournamentFormat.ConfederationsCup:
                            team.ConfederationsCupTitles++;
                            break;
                        case TournamentFormat.ConfederationTournament:
                            team.ConfederationTournamentTitles++;
                            break;
                    }

                    _teamRepository.Update(team);
                }
            }

            tournament.FinalPositions = true;
            _tournamentRepository.Update(tournament);

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
