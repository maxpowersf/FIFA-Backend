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
using Ranking.Domain.Request;

namespace Ranking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            this._matchService = matchService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _matchService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> GetFiltered(MatchCollectionRequestDTO request)
        {
            return new OkObjectResult(await _matchService.Get(_mapper.Map<MatchCollectionRequest>(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTournament(int id)
        {
            return new OkObjectResult(await _matchService.GetByTournament(id));
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            return new OkObjectResult(await _matchService.GetByTeam(teamId));
        }

        [HttpGet("{team1Id}/{team2Id}")]
        public async Task<IActionResult> GetByTeams(int team1Id, int team2Id, bool worldcup)
        {
            return new OkObjectResult(await _matchService.GetByTeams(team1Id, team2Id, worldcup));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _matchService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MatchDTO matchDTO)
        {
            await _matchService.Add(_mapper.Map<Match>(matchDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] MatchDTO matchDTO)
        {
            _matchService.Update(_mapper.Map<Match>(matchDTO));
            return new OkObjectResult(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetReportMargin()
        {
            return new OkObjectResult(await _matchService.GetReportMargin());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportGoals()
        {
            return new OkObjectResult(await _matchService.GetReportGoals());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportWinning()
        {
            return new OkObjectResult(await _matchService.GetReportWinning());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportUnbeaten()
        {
            return new OkObjectResult(await _matchService.GetReportUnbeaten());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportLosing()
        {
            return new OkObjectResult(await _matchService.GetReportLosing());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportWinningless()
        {
            return new OkObjectResult(await _matchService.GetReportWinningless());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportCleanSheets()
        {
            return new OkObjectResult(await _matchService.GetReportCleanSheets());
        }

        [HttpGet]
        public async Task<IActionResult> GetReportScoreless()
        {
            return new OkObjectResult(await _matchService.GetReportScoreless());
        }
    }
}