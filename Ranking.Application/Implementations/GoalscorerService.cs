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
    public class GoalscorerService : IGoalscorerService
    {
        private readonly IGoalscorerRepository _goalscorerRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerRepository _playerRepository;

        public GoalscorerService(IGoalscorerRepository goalscorerRepository, ITournamentRepository tournamentRepository, IPlayerRepository playerRepository)
        {
            this._goalscorerRepository = goalscorerRepository;
            this._tournamentRepository = tournamentRepository;
            this._playerRepository = playerRepository;
        }

        public async Task<List<Goalscorer>> Get()
        {
            return await _goalscorerRepository.Get();
        }

        public async Task<List<Goalscorer>> GetByTournament(int id)
        {
            return await _goalscorerRepository.GetByTournament(id);
        }

        public async Task<Goalscorer> Get(int id)
        {
            return await _goalscorerRepository.Get(id);
        }

        public async Task Add(List<Goalscorer> goalscorers)
        {
            Tournament tournament = await _tournamentRepository.Get(goalscorers[0].TournamentID);
            foreach (Goalscorer goalscorer in goalscorers)
            {
                await _goalscorerRepository.Add(goalscorer);

                Player player = await _playerRepository.Get(goalscorer.PlayerID);
                switch (tournament.TournamentType.Format)
                {
                    case TournamentFormat.Qualification:
                        player.QualificationGoals += goalscorer.Goals;
                        break;
                    case TournamentFormat.WorldCup:
                        player.WorldCupGoals += goalscorer.Goals;
                        if (goalscorer.GoldenBoot) player.WorldCupGoldenBoots++;
                        break;
                    case TournamentFormat.ConfederationsCup:
                        player.ConfederationsGoals += goalscorer.Goals;
                        if (goalscorer.GoldenBoot) player.ConfederationsGoldenBoots++;
                        break;
                    case TournamentFormat.ConfederationTournament:
                        player.ConfederationTournamentGoals += goalscorer.Goals;
                        break;
                }

                _playerRepository.Update(player);
            }

            await _goalscorerRepository.SaveChanges();
        }

        public async Task Update(Goalscorer goalscorer)
        {
            _goalscorerRepository.Update(goalscorer);
            await _goalscorerRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _goalscorerRepository.Delete(id);
            await _goalscorerRepository.SaveChanges();
        }
    }
}
