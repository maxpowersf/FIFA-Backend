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
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Add([FromBody] MatchDTO matchDTO)
        {
            await _rankingService.Add(_mapper.Map<Match>(matchDTO));
            return new OkObjectResult(true);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            await _teamService.UpdateRankings();
            return new OkObjectResult(true);
        }
    }
}