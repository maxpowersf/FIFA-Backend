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
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            this._teamService = teamService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _teamService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _teamService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeamDTO teamDTO)
        {
            await _teamService.Add(_mapper.Map<Team>(teamDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TeamDTO teamDTO)
        {
            _teamService.Update(_mapper.Map<Team>(teamDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}