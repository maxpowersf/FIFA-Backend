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
    public class H2HWorldCupController : ControllerBase
    {
        private readonly IH2HWorldCupService _h2HWorldCupService;
        private readonly IMapper _mapper;

        public H2HWorldCupController(IH2HWorldCupService h2HWorldCupService, IMapper mapper)
        {
            this._h2HWorldCupService = h2HWorldCupService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _h2HWorldCupService.Get());
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            return new OkObjectResult(await _h2HWorldCupService.GetByTeam(teamId));
        }

        [HttpGet("{team1Id}/{team2Id}")]
        public async Task<IActionResult> GetByTeams(int team1Id, int team2Id)
        {
            return new OkObjectResult(await _h2HWorldCupService.GetByTeams(team1Id, team2Id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _h2HWorldCupService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] H2HWorldCupDTO h2hWorldCupDTO)
        {
            await _h2HWorldCupService.Add(_mapper.Map<Head2HeadWorldCup>(h2hWorldCupDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] H2HWorldCupDTO h2hWorldCupDTO)
        {
            _h2HWorldCupService.Update(_mapper.Map<Head2HeadWorldCup>(h2hWorldCupDTO));
            return new OkObjectResult(true);
        }
    }
}