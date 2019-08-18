﻿using Ranking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ranking.Application.Repositories
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> Get();
        Task<Tournament> Get(int id);
        Task Add(Tournament tournament);
        void Update(Tournament tournament);
        Task Delete(int id);
        Task SaveChanges();
    }
}