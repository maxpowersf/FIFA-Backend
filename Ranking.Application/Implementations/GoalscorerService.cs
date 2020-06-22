using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
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

        public GoalscorerService(IGoalscorerRepository goalscorerRepository, ITournamentRepository tournamentRepository)
        {
            this._goalscorerRepository = goalscorerRepository;
            this._tournamentRepository = tournamentRepository;
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

                /*if (position.Qualified || position.NoPosition == 1)
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
                }*/
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
