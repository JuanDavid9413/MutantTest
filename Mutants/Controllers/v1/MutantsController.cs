using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Mutants.Entities.interfaces.business;
using BackEnd.Mutants.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mutants.Controllers.v1
{
    [Route("api/Mutants")]
    [ApiController]
    public class MutantsController : ControllerBase
    {
        private readonly IMutantsBusiness _business;
        public MutantsController(IMutantsBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("stats")]
        public async Task<ActionResult> GetMutant()
        {
            var response = await _business.GetMutants();
            return StatusCode(response.Code, response);
        }

        [HttpPost]
        [Route("mutant")]
        public async Task<ActionResult> IsMutant([FromBody] DnaRequest dna)
        {
            var response = await _business.IsMutant(dna);
            return StatusCode(response.Code, response);
        }
    }
}
