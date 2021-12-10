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
        public ObservationController (SigimacContext context)
        {
            _context = context;
        }

        private readonly SigimacContext _context;

        [HttpGet("{reference}")]

        public async Task<List<Observation>> GetObservationsAsync(int reference)
        {
            List<Observation> Obser = await Task.Run(()=> _context.Observations
                .Where(e => e.IdTmpArma == reference).OrderBy(e => e.FechaObservation)
                .Select(e => new Observation
                {
                    IdTmpArma = e.IdTmpArma,
                    FechaObservation = e.FechaObservation,
                    Observacion = e.Observacion

                }).ToList());

            return Obser;
        }
    }
}
