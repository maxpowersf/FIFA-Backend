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
    public class ConfederationController : ControllerBase
    {
        private readonly IConfederationService _confederationService;
        private readonly IMapper _mapper;

        public ConfederationController(IConfederationService confederationService, IMapper mapper)
        {
            this._confederationService = confederationService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _confederationService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _confederationService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ConfederationDTO confederationDTO)
        {
            await _confederationService.Add(_mapper.Map<Confederation>(confederationDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ConfederationDTO confederationDTO)
        {
            await _confederationService.Update(_mapper.Map<Confederation>(confederationDTO));
            return new OkObjectResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _confederationService.Delete(id);
            return new OkObjectResult(true);
        }
    }
}