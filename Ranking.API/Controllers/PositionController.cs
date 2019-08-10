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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionController(IPositionService positionService, IMapper mapper)
        {
            this._positionService = positionService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTeam(int id)
        {
            return new OkObjectResult(await _positionService.GetByTeam(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTournament(int id)
        {
            return new OkObjectResult(await _positionService.GetByTournament(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _positionService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _positionService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PositionDTO positionDTO)
        {
            await _positionService.Add(_mapper.Map<Position>(positionDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PositionDTO positionDTO)
        {
            _positionService.Update(_mapper.Map<Position>(positionDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}