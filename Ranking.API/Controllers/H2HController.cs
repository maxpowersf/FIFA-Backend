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
    public class H2HController : ControllerBase
    {
        private readonly IH2HService _h2HService;
        private readonly IMapper _mapper;

        public H2HController(IH2HService h2HService, IMapper mapper)
        {
            this._h2HService = h2HService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _h2HService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await _h2HService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] H2HDTO h2hDTO)
        {
            await _h2HService.Add(_mapper.Map<Head2Head>(h2hDTO));
            return new OkObjectResult(true);
        }

        [HttpPut]
        public IActionResult Update([FromBody] H2HDTO h2hDTO)
        {
            _h2HService.Update(_mapper.Map<Head2Head>(h2hDTO));
            return new OkObjectResult(true);
        }
    }
}