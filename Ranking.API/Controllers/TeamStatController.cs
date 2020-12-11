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
    public class TeamStatController : ControllerBase
    {
        private readonly ITeamStatService _teamStatService;
        private readonly IMapper _mapper;

        public TeamStatController(ITeamStatService teamStatService, IMapper mapper)
        {
            this._teamStatService = teamStatService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _teamStatService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _teamStatService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeamStatDTO teamStatDTO)
        {
            await _teamStatService.Add(_mapper.Map<TeamStat>(teamStatDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TeamStatDTO teamStatDTO)
        {
            _teamStatService.Update(_mapper.Map<TeamStat>(teamStatDTO));
            return new OkObjectResult(true);
        }
    }
}