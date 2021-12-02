using ApiSfCuim.Data.Context;
using ApiSfCuim.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSfCuim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultController : ControllerBase
    {
        private readonly RenarContext _context;

        public ConsultController (RenarContext context)
        {
            _context = context;
        }

        [HttpGet("{idArma}")]

        public async Task<ActionResult<Consult>> GetConsults(int? idArma)
        {
            var consult = await _context.Consults.FindAsync(idArma);

            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        } 

    }
}
