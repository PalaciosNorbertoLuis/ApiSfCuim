using ApiSfCuim.Data.Context;
using ApiSfCuim.Domain.Models;
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
    public class SfViewConsultaArmaController : ControllerBase
    {
        private readonly SfViewConsultaArmaContext _context;

        public SfViewConsultaArmaController(SfViewConsultaArmaContext context)
        {
            _context = context;
        }

        // GET: api/SF_VIEW_CONSULTA_ARMA/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SfViewConsultaArma>> GetSF_VIEW_CONSULTA_ARMA(int? id)
        {
            var sF_VIEW_CONSULTA_ARMA = await _context.SF_VIEW_CONSULTA_ARMA.FindAsync(id);

            if (sF_VIEW_CONSULTA_ARMA == null)
            {
                return NotFound();
            }

            return sF_VIEW_CONSULTA_ARMA;
        }


    }
}
