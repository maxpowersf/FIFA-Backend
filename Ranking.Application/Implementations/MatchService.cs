﻿using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Domain;
using Ranking.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            this._matchRepository = matchRepository;
        }

        public async Task Add(Match match)
        {
            await _matchRepository.Add(match);
            await _matchRepository.SaveChanges();
        }

        public Task<List<Match>> Get()
        {
            return _matchRepository.Get();
        }

        public Task<List<Match>> Get(MatchCollectionRequest request)
        {
            return _matchRepository.Get(request);
        }

        public Task<Match> Get(int id)
        {
            return _matchRepository.Get(id);
        }

        public async Task<List<Match>> GetByTournament(int id)
        {
            return await _matchRepository.GetByTournament(id);
        }

        public Task<List<Match>> GetByTeam(int teamId)
        {
            return _matchRepository.GetByTeam(teamId);
        }

        public Task<List<Match>> GetByTeams(int team1Id, int team2Id, bool worldcup)
        {
            return _matchRepository.GetByTeams(team1Id, team2Id, worldcup);
        }

        public async Task Update(Match match)
        {
            _matchRepository.Update(match);
            await _matchRepository.SaveChanges();
        }
    }
}