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
    public class FilterController : ControllerBase
    {
        private readonly SigimacContext _context;
        private readonly RenarAuxiliarContext _context2;

        public FilterController(SigimacContext context, RenarAuxiliarContext context2)
        {
            _context = context;
            _context2 = context2;
        }


        //public FilterController(RenarAuxiliarContext context2)
        //{
        //    _context2 = context2;
        //}

        [HttpGet("{reference}")]

        public IActionResult Index(int reference)
        {
            List<Filter> filters = _context.Filters
                                .Where(e => e.IdTmpArma == reference).OrderBy(e => e.FechaFiltro)
                                .Select(e => new Filter
                                {
                                    IdTmpArma          = e.IdTmpArma,
                                    FechaFiltro        = e.FechaFiltro,
                                    Filtro             = e.Filtro,
                                    TipoOperadorFiltro = e.TipoOperadorFiltro,
                                    IdOperadorFiltro   = e.IdOperadorFiltro
                                }).ToList();

            foreach(var f in filters)
            {
                var operador = _context2.Operators.Where(e => e.id_usuario == f.IdOperadorFiltro).Select(e => e.login_id);
                f.Operador = operador.Single();
            }

            return Ok(filters);
                                
        }


    }
}
