using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ranking.API.DTO;
using Ranking.Application.Interfaces;
using Ranking.Domain;

namespace Ranking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoalscorerController : ControllerBase
    {
        private readonly IGoalscorerService _goalscorerService;
        private readonly IMapper _mapper;

        public GoalscorerController(IGoalscorerService goalscorerService, IMapper mapper)
        {
            this._goalscorerService = goalscorerService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTournament(int id)
        {
            return new OkObjectResult(await _goalscorerService.GetByTournament(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _goalscorerService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _goalscorerService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GoalscorerArrayDTO goalscorersDTO)
        {
            await _goalscorerService.Add(_mapper.Map<List<Goalscorer>>(goalscorersDTO.Goalscorer));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] GoalscorerDTO goalscorerDTO)
        {
            _goalscorerService.Update(_mapper.Map<Goalscorer>(goalscorerDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _goalscorerService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}