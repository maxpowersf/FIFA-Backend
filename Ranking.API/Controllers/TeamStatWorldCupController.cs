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
    public class TeamStatWorldCupController : ControllerBase
    {
        private readonly ITeamStatWorldCupService _teamStatWorldCupService;
        private readonly IMapper _mapper;

        public TeamStatWorldCupController(ITeamStatWorldCupService teamStatWorldCupService, IMapper mapper)
        {
            this._teamStatWorldCupService = teamStatWorldCupService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _teamStatWorldCupService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _teamStatWorldCupService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeamStatWorldCupDTO teamStatWorldCupDTO)
        {
            await _teamStatWorldCupService.Add(_mapper.Map<TeamStatWorldCup>(teamStatWorldCupDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TeamStatWorldCupDTO teamStatWorldCupDTO)
        {
            _teamStatWorldCupService.Update(_mapper.Map<TeamStatWorldCup>(teamStatWorldCupDTO));
            return new OkObjectResult(true);
        }
    }
}