using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ranking.API.DTO;
using Ranking.Application.Interfaces;
using Ranking.Domain;

namespace Ranking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public RankingController(IRankingService rankingService, ITeamService teamService, IMapper mapper)
        {
            this._rankingService = rankingService;
            this._teamService = teamService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddMatchRanking([FromBody] MatchDTO matchDTO)
        {
            await _rankingService.AddMatchRanking(_mapper.Map<MatchRanking>(matchDTO));
            return new OkObjectResult(true);
        }

        [HttpPost]
        public async Task<IActionResult> AddMatch([FromBody] MatchDTO matchDTO)
        {
            await _rankingService.AddMatch(_mapper.Map<Match>(matchDTO));
            return new OkObjectResult(true);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            await _teamService.UpdateRankings();
            return new OkObjectResult(true);
        }

        [HttpGet]
        public async Task<IActionResult> FinishPeriod()
        {
            await _rankingService.FinishPeriod();
            return new OkObjectResult(true);
        }
    }
}