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
    public class ObservationController : ControllerBase
    {
        private readonly SigimacContext _context;
        public ObservationController (SigimacContext context)
        {
            _context = context;
        }

        [HttpGet("{reference}")]

        public IActionResult Index(int reference)
        {
            List<Observation> Obser = _context.Observations
                .Where(e => e.IdTmpArma == reference).OrderBy(e=> e.FechaObservation)
                .Select(e => new Observation
                {
                    IdTmpArma   = e.IdTmpArma,
                    FechaObservation = e.FechaObservation,
                    Observacion = e.Observacion

                }).ToList();

            return Ok(Obser);
        }
    }
}
