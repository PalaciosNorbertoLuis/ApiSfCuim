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
    public class FilterController : ControllerBase
    {
        private readonly SigimacContext _context;

        public FilterController (SigimacContext context)
        {
            _context = context;
        }

        [HttpGet("{reference}")]

        public IActionResult Index(int reference)
        {
            List<Filter> filter = _context.Filters
                                .Where(e => e.IdTmpArma == reference)
                                .Select(e => new Filter
                                {
                                    IdTmpArma          = e.IdTmpArma,
                                    FechaFiltro        = e.FechaFiltro,
                                    Filtro             = e.Filtro,
                                    TipoOperadorFiltro = e.TipoOperadorFiltro,
                                    IdOperadorFiltro   = e.IdOperadorFiltro
                                }).ToList();
            return Ok(filter);
                                
        }


    }
}
