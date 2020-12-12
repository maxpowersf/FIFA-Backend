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
    public class TournamentTypeController : ControllerBase
    {
        private readonly ITournamentTypeService _tournamentTypeService;
        private readonly IMapper _mapper;

        public TournamentTypeController(ITournamentTypeService tournamentTypeService, IMapper mapper)
        {
            this._tournamentTypeService = tournamentTypeService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _tournamentTypeService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _tournamentTypeService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TournamentTypeDTO tournamentTypeDTO)
        {
            await _tournamentTypeService.Add(_mapper.Map<TournamentType>(tournamentTypeDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TournamentTypeDTO tournamentTypeDTO)
        {
            await _tournamentTypeService.Update(_mapper.Map<TournamentType>(tournamentTypeDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tournamentTypeService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}