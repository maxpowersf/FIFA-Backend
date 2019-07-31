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
    public class MatchTypeController : ControllerBase
    {
        private readonly IMatchTypeService _matchTypeService;
        private readonly IMapper _mapper;

        public MatchTypeController(IMatchTypeService matchTypeService, IMapper mapper)
        {
            this._matchTypeService = matchTypeService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _matchTypeService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _matchTypeService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MatchTypeDTO matchTypeDTO)
        {
            await _matchTypeService.Add(_mapper.Map<MatchType>(matchTypeDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] MatchTypeDTO matchTypeDTO)
        {
            _matchTypeService.Update(_mapper.Map<MatchType>(matchTypeDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _matchTypeService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}