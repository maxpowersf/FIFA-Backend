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
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IMapper _mapper;

        public TournamentController(ITournamentService tournamentService, IMapper mapper)
        {
            this._tournamentService = tournamentService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _tournamentService.Get());
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            return new OkObjectResult(await _tournamentService.GetByTeam(teamId));
        }

        [HttpGet("{tournamentTypeId}")]
        public async Task<IActionResult> GetByTournamentTypeWithPositions(int tournamentTypeId)
        {
            return new OkObjectResult(await _tournamentService.GetByTournamentTypeWithPositions(tournamentTypeId));
        }

        [HttpGet("{tournamentTypeId}/{confederationId:int?}")]
        public async Task<IActionResult> GetByTournamentTypeAndConfederation(int tournamentTypeId, int confederationId)
        {
            return new OkObjectResult(await _tournamentService.GetByTournamentTypeAndConfederation(tournamentTypeId, confederationId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrentStandings(int id)
        {
            return new OkObjectResult(await _tournamentService.GetCurrentStandings(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFinalTable(int id)
        {
            return new OkObjectResult(await _tournamentService.GetFinalTable(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _tournamentService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TournamentDTO tournamentDTO)
        {
            await _tournamentService.Add(_mapper.Map<Tournament>(tournamentDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TournamentDTO tournamentDTO)
        {
            _tournamentService.Update(_mapper.Map<Tournament>(tournamentDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tournamentService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}