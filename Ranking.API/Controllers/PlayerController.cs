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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            this._playerService = playerService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _playerService.Get());
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            return new OkObjectResult(await _playerService.GetByTeam(teamId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _playerService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PlayerDTO playerDTO)
        {
            await _playerService.Add(_mapper.Map<Player>(playerDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlayerDTO playerDTO)
        {
            _playerService.Update(_mapper.Map<Player>(playerDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _playerService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}