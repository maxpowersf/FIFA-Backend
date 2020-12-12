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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTournament(int id)
        {
            return new OkObjectResult(await _matchService.GetByTournament(id));
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
    }
}